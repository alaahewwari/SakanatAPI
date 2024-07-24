using AutoMapper;
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ApartmentDtos.Requests;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Helpers;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.ApartmentServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.NotificationServices.Interfaces;
using BusinessLogic.Services.StorageServices.Interfaces;
using BusinessLogic.Services.UserServices.Interfaces;
using DataAccess.Enums.Notification;
using DataAccess.Models;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Sieve.Models;
using Apartment = DataAccess.Models.Apartment;
namespace BusinessLogic.Services.ApartmentServices.Implementations;

public class ApartmentServices(
    IApartmentRepository apartmentRepository,
    IIdentityManager identityManager,
    IMapper mapper,
    ICloudinaryServices cloudinaryServices,
    ISuspensionServices suspensionServices,
    ICityRepository cityRepository,
    IUniversityRepository universityRepository,
    INotificationServices notificationServices,
    IFollowingRepository followingRepository,
    IUnitOfWork unitOfWork
    )
    : IApartmentServices
{

    public async Task<ErrorOr<PagedResult<ApartmentOverviewResponseDto>>> GetAllApartments(SieveModel sieveModel)
    {
        var apartments = await apartmentRepository.GetAllApartmentsAsync(sieveModel);
        return apartments;
    }

    public async Task<ErrorOr<IList<ApartmentResponseDto>>> GetApartmentsByUserId(Guid id, SieveModel sieveModel)
    {
        var user = await identityManager.FindByIdAsync(id);
        if (user is null)
            return Errors.Identity.UserNotFound;

        var apartments = await apartmentRepository.GetApartmentsByUserId(id, sieveModel);

        if (apartments is null)
            return Errors.Apartment.NoApartmentsFound;

        return apartments.ToList();
    }
    public async Task<ErrorOr<CreateApartmentResponseDto>> CreateApartmentAsync(ApartmentRequestDto request)
    {
        var user = await identityManager.GetLoggedInUserIdAsync();
        if (user is null)
            return Errors.Identity.Unauthorized;

        var isSuspended = await suspensionServices.GetSuspendedUserByIdAsync(user.Id);
        if (!isSuspended.IsError)
        {
            return Errors.Suspension.UserIsSuspended;
        }
        var city = await cityRepository.GetCityByNameAsync(request.CityName);
        if (city is null)
            return Errors.City.CityNotFound;

        var university = await universityRepository.GetUniversityByNameAsync(request.UniversityName);
        if (university is null)
            return Errors.University.UniversityNotFound;

        if (request.PriceCurrency == 0 || request.FurnishedStatus == 0 || request.GenderAllowed == 0)
            return Errors.Apartment.InvalidInfo;
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var apartment = mapper.Map<Apartment>(request);
            apartment.UserId = user.Id;
            apartment.CityId = city.Id;
            apartment.UniversityId = university.Id;
            apartment.CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var response = await apartmentRepository.CreateApartmentAsync(apartment, user);
            if (response is null)
                return Errors.Apartment.ApartmentAlreadyExist;
            var followers = await followingRepository.GetFollowersAsync(user,new SieveModel());
            var followersIds = followers.Select(f => f.Id).ToList();
            await notificationServices.CreateNotificationAsync(NotificationType.NewApartment, user.Id, followersIds, apartment.Id);
            await unitOfWork.CommitTransactionAsync();
            return new CreateApartmentResponseDto
            {
                Id = response.Id,
                message = "Apartment Created Successfully"
            };
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackTransactionAsync();
            return Errors.Unknown.Create(e.Message);
        }
    }
    public async Task<ErrorOr<ApartmentResponseDto>> GetApartmentByIdAsync(Guid id)
    {
        var apartment = await apartmentRepository.GetApartmentByIdAsync(id);

        if (apartment == null)
            return Errors.Apartment.ApartmentNotFound;


        return apartment;
    }
    public async Task<ErrorOr<SuccessResponse>> UpdateApartmentAsync(Guid id, ApartmentToUpdateRequestDto request)
    {
        var apartment = await apartmentRepository.GetApartmentByIdAsync(id);

        if (apartment == null)
            return Errors.Apartment.ApartmentNotFound;

        var result = mapper.Map<Apartment>(request);

        var response = await apartmentRepository.UpdateApartmentAsync(id, result);

        if (response is null)
            return Errors.Apartment.FailedToUpdateApartment;

        return new SuccessResponse("Apartment Updated Successfully");
    }
    public async Task<ErrorOr<SuccessResponse>> DeleteApartmentAsync(Guid id)
    {
        var apartment = await apartmentRepository.GetApartmentByIdAsync(id);

        if (apartment == null)
            return Errors.Apartment.ApartmentNotFound;

        var response = await apartmentRepository.DeleteApartmentAsync(id);

        if (response is null)
            return Errors.Apartment.FailedToDeleteApartment;

        return new SuccessResponse("Apartment Deleted Successfully");
    }
    public async Task<ErrorOr<IList<ApartmentImageResponseDto>>> UploadImagesAsync(List<IFormFile> images, Guid apartmentId)
    {
        List<ApartmentImageResponseDto> uploadedImages = new List<ApartmentImageResponseDto>();
        var apartmentImages = await apartmentRepository.GetApartmentImagesAsync(apartmentId);
        bool isFirstImage = true;
        foreach (var image in images)
        {
            var uploadResult = await cloudinaryServices.UploadImageAsync(image.OpenReadStream(), image.FileName);
            if (uploadResult.Url != null)
            {
                var apartmentImage = new ApartmentImage
                {
                    ApartmentId = apartmentId,
                    ImagePath = uploadResult.Url.ToString(),
                    IsCover = apartmentImages.Count == 0 ? isFirstImage : false
                };
                var result = await apartmentRepository.AddApartmentImageAsync(apartmentImage);
                uploadedImages.Add(result);
                isFirstImage = false;
            }
            else
            {
                return Errors.Image.FailedToUploadImage;
            }
        }

        return uploadedImages.ToList();
    }
    public async Task<ErrorOr<IList<ApartmentImageResponseDto>>> GetApartmentImagesAsync(Guid id)
    {
        var images = await apartmentRepository.GetApartmentImagesAsync(id);
        if (images is null)
            return Errors.Image.NoImagesFound;

        var response = mapper.Map<IList<ApartmentImageResponseDto>>(images);

        if (response is null)
            return Errors.Image.NoImagesFound;

        return response.ToList();
    }
    public async Task<ErrorOr<ApartmentImageResponseDto>> UpdateApartmentImageAsync(Guid apartmentId, Guid imageId, IFormFile file)
    {
        var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
        if (apartment == null)
            return Errors.Apartment.ApartmentNotFound;
        var images = await apartmentRepository.GetApartmentImagesAsync(apartmentId);
        if (images is null)
            return Errors.Image.NoImagesFound;

        var image = images.FirstOrDefault(i => i.Id == imageId);

        if (image == null)
            return Errors.Image.ImageNotFound;
        if (file == null || file.Length == 0)
        {
            return Errors.Image.ImageNotFound;
        }

        using (var fileStream = file.OpenReadStream())
        {
            var parser = new CloudinaryUrlParser();
            var publicId = parser.ExtractPublicIdFromUrl(image.ImagePath);
            var deletionResult = await cloudinaryServices.DeleteImageAsync(publicId);
            if (!deletionResult.Result.Equals("ok"))
            {
                return Errors.Unknown.Create("Failed to delete previous image");
            }
            var fileName = file.FileName;
            var uploadResult = await cloudinaryServices.UploadImageAsync(fileStream, fileName);
            if (uploadResult == null || uploadResult.Url == null)
            {
                return Errors.Image.FailedToUploadImage;
            }
            image.ImagePath = uploadResult.Url.ToString();
            var updatedImage = await apartmentRepository.UpdateApartmentImageAsync(image);
            await unitOfWork.SaveChangesAsync();
        }
        var response = mapper.Map<ApartmentImageResponseDto>(image);
        return response;
    }
    public async Task<ErrorOr<ApartmentImageResponseDto>> GetApartmentImageByIdAsync(Guid imageId)
    {
        var image = await apartmentRepository.GetApartmentImageByIdAsync(imageId);
        if (image == null)
            return Errors.Image.ImageNotFound;

        var response = mapper.Map<ApartmentImageResponseDto>(image);
        return response;
    }
    public async Task<ErrorOr<SuccessResponse>> DeleteApartmentImageAsync(Guid apartmentId, Guid imageId)
    {
        var image = await apartmentRepository.GetApartmentImageByIdAsync(imageId);
        if (image == null)
            return Errors.Image.ImageNotFound;

        var parser = new CloudinaryUrlParser();
        var publicId = parser.ExtractPublicIdFromUrl(image.ImagePath);
        var deletionResult = await cloudinaryServices.DeleteImageAsync(publicId);
        if (!deletionResult.Result.Equals("ok"))
        {
            return Errors.Unknown.Create("Failed to delete image from Cloudinary");
        }
        var response = await apartmentRepository.DeleteApartmentImageAsync(apartmentId, imageId);
        if (!response)
        {
            return Errors.Unknown.Create("Failed to delete image from Database");
        }

        return new SuccessResponse("Image Deleted Successfully");
    }
    public async Task<ErrorOr<IList<ApartmentDiscountsResponseDto>>> GetUserApartmentDiscounts(Guid userId)
    {
        var user = await identityManager.FindByIdAsync(userId);
        if (user is null)
            return Errors.Identity.UserNotFound;
        var response = await apartmentRepository.GetAllDiscountsForApartmentsByUserIdAsync(userId);
        if (response is null)
        {
            return Errors.Discount.DiscountNotFound;
        }
        return response.ToList();
    }
    public async Task<ApartmentWithDiscountsNumberResponseDto> GetActiveApartmentWithDiscountsNumberAsync()
    {
        var response = await apartmentRepository.GetActiveApartmentWithDiscountsNumberAsync();
        return new ApartmentWithDiscountsNumberResponseDto
        {
            ApartmentNumber = response
        };
    }
public async Task<ErrorOr<SuccessResponse>> ChangeApartmentAvailabilityAsync(Guid apartmentId)
    {
var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
        if (apartment is null) {
            return Errors.Apartment.ApartmentNotFound;
        }
        var response = await apartmentRepository.ChangeApartmentAvailabilityAsync(apartmentId);
        if (!response)
        {
            return Errors.Unknown.Create("Failed to change apartment availability");
        }
        return new SuccessResponse("Apartment availability changed successfully");
    }
    public async Task<ErrorOr<SuccessResponse>> ChangeApartmentVisibilityAsync(Guid apartmentId)
    {
        var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
        if (apartment is null)
        {
            return Errors.Apartment.ApartmentNotFound;
        }
        var response = await apartmentRepository.ChangeApartmentVisibilityAsync(apartmentId);
        if (!response)
        {
            return Errors.Unknown.Create("Failed to change apartment visibility");
        }
        return new SuccessResponse("Apartment visibility changed successfully");
    }

}
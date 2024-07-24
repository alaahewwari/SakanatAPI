using AutoMapper;
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Helpers;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.StorageServices.Interfaces;
using BusinessLogic.Services.UserServices.Interfaces;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Sieve.Models;

namespace BusinessLogic.Services.UserServices.Implementations;

public class UserServices(
    IMapper mapper,
    ICloudinaryServices cloudinaryServices,
    IIdentityManager identityManager,
    IUnitOfWork unitOfWork,
    ICityRepository cityRepository,
    IUserRepository userRepository
    )
    : IUserServices
{
    public async Task<ErrorOr<PagedResult<GetUserResponseDto>>> GetAllUsersAsync(SieveModel sieveModel,string? fullName)
    {
        var users = await userRepository.GetAllUsersAsync(sieveModel,fullName);
        return users;
    }
    public async Task<ErrorOr<ProfileImageResponseDto>> UpdateProfileImageAsync(IFormFile? file)
    {
        if (IsImage(file).Result == false)
        {
            return Errors.Image.InvalidImage;
        }

        var user =await identityManager.GetLoggedInUserIdAsync();

        if (user == null)
        {
            return Errors.Identity.Unauthorized;
        }

        await using (var fileStream = file.OpenReadStream())
        {
            if (user.ImagePath != null)
            {
                var parser = new CloudinaryUrlParser();
                var publicId =parser.ExtractPublicIdFromUrl(user.ImagePath);
                Console.WriteLine("Public ID extracted: " + publicId);
                var deletionResult = await cloudinaryServices.DeleteImageAsync(publicId);
                if (!deletionResult.Result.Equals("ok"))
                {
                    return Errors.Unknown.Create("Failed to delete previous image");
                }
            }
            var fileName = file.FileName;
            var uploadResult = await cloudinaryServices.UploadImageAsync(fileStream, fileName);
            if (uploadResult == null || uploadResult.Url == null)
            {
                return Errors.Image.FailedToUploadImage;
            }
            user.ImagePath = uploadResult.Url.ToString();
            await unitOfWork.SaveChangesAsync();
        }
        return new ProfileImageResponseDto
        {
            ImagePath = user.ImagePath
        };
    }
    public async Task<ErrorOr<SuccessResponse>> DeleteProfileImageAsync()
    {
        var user = await identityManager.GetLoggedInUserIdAsync();
        if (user == null)
        {
            return Errors.Identity.Unauthorized;
        }
        if (user.ImagePath == null)
        {
            return Errors.Image.ImageNotFound;
        }
        var parser = new CloudinaryUrlParser();
        var publicId = parser.ExtractPublicIdFromUrl(user.ImagePath);
        var deletionResult = await cloudinaryServices.DeleteImageAsync(publicId);
        if (!deletionResult.Result.Equals("ok"))
        {
            return Errors.Unknown.Create("Failed to delete image from Cloudinary");
        }
        var response = await userRepository.DeleteProfileImageAsync(user);
        if (!response)
        {
            return Errors.Unknown.Create("Failed to delete image from Database");
        }
        return new SuccessResponse("Image Deleted Successfully");
    }
    public async Task<ErrorOr<UserOverviewResponseDto>> GetUserByIdAsync(Guid userId)
    {
        var user = await identityManager.GetUserByIdAsync(userId);
        if (user is null)
        {
            return Errors.Identity.UserNotFound;
        }
        return user;
    }
    public async Task<ErrorOr<GetUserResponseDto>> GetUserByEmailAsync(string email)
    {
        var user = await identityManager.GetUserByEmailAsync(email);
        if (user is null)
        {
            return Errors.Identity.UserNotFound;
        }
        return user;
    }
    public async Task<ErrorOr<SuccessResponse>> ChangePasswordAsync(ChangePasswordRequestDto request)
    {
        var user= await identityManager.GetLoggedInUserIdAsync();
        if (user is null)
        {
            return Errors.Identity.Unauthorized;
        }
        var result = await identityManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result) return Errors.User.PasswordChangeFailed;
        return new SuccessResponse("Password changed successfully");
    }
    public async Task<ErrorOr<SuccessResponse>> UpdateUserAsync(UpdateUserRequestDto request)
    {
        var user = await identityManager.GetLoggedInUserIdAsync();

        if (user is null)
        {
            return Errors.Identity.Unauthorized;
        }

        var city = await cityRepository.GetCityByNameAsync(request.City);

        if (city is null)
        {
            return Errors.City.CityNotFound;
        }

        if (user is null)
        {
            return Errors.Identity.UserNotFound;
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.PhoneNumber = request.PhoneNumber;
        user.CityId = city.Id;

        await identityManager.UpdateAsync(user);
        return new SuccessResponse("User updated successfully");
    }
    public async Task<ErrorOr<SuccessResponse>> DeleteAccountAsync()
    {
        var user = await identityManager.GetLoggedInUserIdAsync();
        if (user is null)
        {
            return Errors.Identity.Unauthorized;
        }
        await userRepository.DeleteUserAsync(user);
        return new SuccessResponse("User deleted successfully");
    }
    private async Task<bool> IsImage(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return false;
        }

        // Define a list of valid image file extensions
        var validExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg", ".jpeg", ".png", ".gif"
        };

        // Extract the file extension from the uploaded file
        var fileExtension = Path.GetExtension(file.FileName);

        // Check if the file extension is in the list of valid image extensions
        return validExtensions.Contains(fileExtension);
    }
}
using AutoMapper;
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.DiscountDtos.Requests;
using BusinessLogic.DTOs.DiscountDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.DiscountsServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.NotificationServices.Interfaces;
using DataAccess.Enums.Notification;
using ErrorOr;
using Sieve.Models;
using Discount = DataAccess.Models.Discount;
namespace BusinessLogic.Services.DiscountsServices.Implementations
{
    public class DiscountServices(
        IDiscountRepository discountRepository,
        IApartmentRepository apartmentRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IFollowingRepository followingRepository,
        INotificationServices notificationServices,
        IIdentityManager identityManager)
        : IDiscountServices
    {
        public async Task<ErrorOr<IList<GetApartmentDiscountsResponseDto>>> GetAllDiscountsByApartmentIdAsync(SieveModel sieveModel, Guid apartmentId)
        {
            var apartment= await apartmentRepository.GetApartmentByIdAsync(apartmentId);
            if (apartment is null)
            {
                return Errors.Apartment.ApartmentNotFound;
            }
            var discounts = await discountRepository.GetAllDiscountsByApartmentIdAsync(sieveModel, apartmentId);
            
            return discounts.ToList();
        }
        public async Task<ErrorOr<PagedResult<DiscountResponseDto>>> GetAllDiscountsForUserAsync(Guid userId,
            SieveModel sieveModel)
        {
            var discounts = await discountRepository.GetAllDiscountsForUserAsync(userId, sieveModel);
            return discounts;
        }
        public async Task<ErrorOr<DiscountResponseDto>> GetDiscountById(Guid id)
        {
            var discount = await discountRepository.GetDiscountByIdAsync(id);
            if (discount is null)
            {
                return Errors.Discount.DiscountNotFound;
            }
            var response = mapper.Map<DiscountResponseDto>(discount);
            var isAdded = await discountRepository.IsAddedAsync(id);
            var apartmentCount = await discountRepository.GetNumberOfApartmentsWithDiscountByIdAsync(id);
            response.IsAdded = isAdded;
            response.ApartmentsCount = apartmentCount;
            return response;
        }
        public async Task<ErrorOr<DiscountResponseDto>> CreateDiscountAsync(DiscountRequestDto request)
        {
            var user =await identityManager.GetLoggedInUserIdAsync();
            if (user is null)
                return Errors.Identity.Unauthorized;

            var discount = mapper.Map<Discount>(request);
            discount.UserId = user.Id;
            discount.CreationDate = DateOnly.FromDateTime(DateTime.Now);
            var createdDiscount = await discountRepository.CreateDiscountAsync(discount);
            var response = mapper.Map<DiscountResponseDto>(createdDiscount);
            response.IsAdded = false;
            return response;
        }
        public async Task<ErrorOr<SuccessResponse>> AddDiscountForApartmentAsync(Guid apartmentId, Guid discountId, DateOnly expiresAt)
        {
            var user = await identityManager.GetLoggedInUserIdAsync();
            if (user is null)
                return Errors.Identity.Unauthorized;

            var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
            if (apartment is null)
                return Errors.Apartment.ApartmentNotFound;

            var discount = await discountRepository.GetDiscountByIdAsync(discountId);
            if(discount is null)
                return Errors.Discount.DiscountNotFound;

            var apartmentDiscounts = await discountRepository.GetAllDiscountsByApartmentIdAsync(new SieveModel(), apartmentId);
            if (apartmentDiscounts.Any(d => d.Id == discountId))
                return Errors.Discount.DiscountAlreadyAdded;

            try
            {
                await unitOfWork.BeginTransactionAsync();
                var isAdded = await discountRepository.AddDiscountForApartmentAsync(apartmentId, discountId, expiresAt);
                var followers = await followingRepository.GetFollowersAsync(user,new SieveModel());
                if (followers.Count > 0)
                {
                    List<Guid> followersIds = followers.Select(f => f.Id).ToList();
                    await notificationServices.CreateNotificationAsync(NotificationType.NewDiscount, user.Id, followersIds, apartmentId);
                }
                await unitOfWork.CommitTransactionAsync();
                return new SuccessResponse("discount added successfully");
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Errors.Unknown.Create("Failed to add discount to apartment");
            }
        }
        public async Task<ErrorOr<SuccessResponse>> RemoveDiscountFromApartmentAsync(Guid apartmentId, Guid discountId)
        {
            var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
            if (apartment is null)
                return Errors.Apartment.ApartmentNotFound;

            var discount = await discountRepository.GetDiscountByIdAsync(discountId);
            if (discount is null)
                return Errors.Discount.DiscountNotFound;

            var isRemoved = await discountRepository.RemoveDiscountFromApartmentAsync(apartmentId, discountId);
            if (!isRemoved)
                return Errors.Unknown.Create("Failed to remove discount from apartment");

            return new SuccessResponse("discount removed successfully");
        }
        public async Task<ErrorOr<SuccessResponse>> UpdateDiscountAsync(Guid id, DiscountRequestDto request)
        {
            var discount = await discountRepository.GetDiscountByIdAsync(id);
            if (discount is null)
            {
                return Errors.Discount.DiscountNotFound;
            }
            var updatedDiscount = mapper.Map<Discount>(request);
            await discountRepository.UpdateDiscountAsync(id, updatedDiscount);
            return new SuccessResponse("Discount Updated Successfully");
        }
        public async Task<ErrorOr<SuccessResponse>> DeleteDiscountAsync(Guid id)
        {
            var discount = await discountRepository.GetDiscountByIdAsync(id);
            if (discount is null)
            {
                return Errors.Discount.DiscountNotFound;
            }
            await discountRepository.DeleteDiscountAsync(id);
            return new SuccessResponse("Discount Deleted Successfully");
        }
    }
}
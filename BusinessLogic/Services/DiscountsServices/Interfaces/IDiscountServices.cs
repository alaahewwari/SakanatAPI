using BusinessLogic.Contracts.General;
using ErrorOr;
using BusinessLogic.DTOs.DiscountDtos.Requests;
using BusinessLogic.DTOs.DiscountDtos.Responses;
using Sieve.Models;
using BusinessLogic.Common.Models;

namespace BusinessLogic.Services.DiscountsServices.Interfaces
{
    public interface IDiscountServices
    {
public Task<ErrorOr<IList<GetApartmentDiscountsResponseDto>>> GetAllDiscountsByApartmentIdAsync(SieveModel sieveModel,
    Guid apartmentId);
public Task<ErrorOr<PagedResult<DiscountResponseDto>>> GetAllDiscountsForUserAsync(Guid userId,SieveModel sieveModel);
public Task<ErrorOr<DiscountResponseDto>> GetDiscountById(Guid id);
Task<ErrorOr<SuccessResponse>> AddDiscountForApartmentAsync(Guid apartmentId, Guid discountId, DateOnly expiresAt);
        public Task<ErrorOr<SuccessResponse>> RemoveDiscountFromApartmentAsync(Guid apartmentId, Guid discountId);
public Task<ErrorOr<DiscountResponseDto>> CreateDiscountAsync(DiscountRequestDto request);
        public Task<ErrorOr<SuccessResponse>> UpdateDiscountAsync(Guid id, DiscountRequestDto request);
public Task<ErrorOr<SuccessResponse>> DeleteDiscountAsync(Guid id);
    }
}

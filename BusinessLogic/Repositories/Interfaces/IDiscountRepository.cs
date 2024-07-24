

using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.DiscountDtos.Responses;
using DataAccess.Models;
using Sieve.Models;
using Sieve.Services;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IDiscountRepository
    {
        Task<IList<GetApartmentDiscountsResponseDto>> GetAllDiscountsByApartmentIdAsync(SieveModel sieveModel,
            Guid apartmentId);
        Task<PagedResult<DiscountResponseDto>> GetAllDiscountsForUserAsync(Guid userId, SieveModel sieveModel);
        Task<Discount?> GetDiscountByIdAsync(Guid id);
        Task<Discount> CreateDiscountAsync(Discount discount);
        Task<bool> AddDiscountForApartmentAsync(Guid apartmentId, Guid discountId, DateOnly expiresAt);
        Task<bool> RemoveDiscountFromApartmentAsync(Guid apartmentId, Guid discountId);
        Task<bool> UpdateDiscountAsync(Guid discountId, Discount discount);
        Task<bool> DeleteDiscountAsync(Guid id);
        Task<bool> IsAddedAsync(Guid id);
        Task<int> GetNumberOfApartmentsWithDiscountByIdAsync (Guid id);
    }
}

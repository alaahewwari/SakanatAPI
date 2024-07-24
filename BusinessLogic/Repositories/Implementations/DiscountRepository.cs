using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.DiscountDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using Discount = DataAccess.Models.Discount;

namespace BusinessLogic.Repositories.Implementations
{
    public class DiscountRepository(
        ApplicationDbContext context,
        ISieveProcessor sieveProcessor
        , IMapper mapper
        ) : IDiscountRepository
    {
        public async Task<IList<GetApartmentDiscountsResponseDto>> GetAllDiscountsByApartmentIdAsync(SieveModel sieveModel, Guid apartmentId)
        {
            var discounts =context.ApartmentDiscounts
                .Where(a => a.ApartmentId == apartmentId)
                .Select(a => a.Discount)
                .AsNoTracking();

            var result = await sieveProcessor.Apply(sieveModel, discounts)
                .ProjectTo<GetApartmentDiscountsResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return result;
        }
        public async Task<PagedResult<DiscountResponseDto>> GetAllDiscountsForUserAsync(Guid userId, SieveModel sieveModel)
        {
            sieveModel.SetDefaultPagination();
            var discountsQuery = context.Users
                .Where(a => a.Id == userId)
                .SelectMany(a => a.Discounts);

            var filteredUsers = sieveProcessor.Apply(sieveModel, discountsQuery, applyPagination: false);
            var totalCount = await filteredUsers.CountAsync();

            var paginatedDiscounts = sieveProcessor.Apply(sieveModel, discountsQuery);
            var projectedDiscounts = await paginatedDiscounts
                .ProjectTo<DiscountResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<DiscountResponseDto>(
                projectedDiscounts,
                totalCount,
                sieveModel.Page!.Value,
                sieveModel.PageSize!.Value
            );
        }
        public async Task<Discount?> GetDiscountByIdAsync(Guid id)
        {
            var discount = await context.Discounts
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return discount;
        }
        public async Task<Discount> CreateDiscountAsync(Discount discount)
        {
            await context.Discounts.AddAsync(discount);
            await context.SaveChangesAsync();
            return discount;
        }
        public async Task<bool> AddDiscountForApartmentAsync(Guid apartmentId, Guid discountId , DateOnly expiresAt)
        {

            var apartment = await context.Apartments
                .Include(u => u.ApartmentDiscounts)
                .FirstOrDefaultAsync(d => d.Id == apartmentId);

            var discounts = await context.Discounts
                .FirstOrDefaultAsync(a => a.Id == discountId);

            if (apartment is null || discounts is null)
                return false;
            await context.ApartmentDiscounts.AddAsync(new ApartmentDiscount
            {
                ApartmentId = apartmentId,
                DiscountId = discountId,
                ExpiresAt = expiresAt
            });
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> RemoveDiscountFromApartmentAsync(Guid apartmentId, Guid discountId)
        {
            var apartmentDiscount = await context.ApartmentDiscounts
                .FirstOrDefaultAsync(a => a.ApartmentId == apartmentId && a.DiscountId == discountId);
            if (apartmentDiscount is null)
                return false;
            context.ApartmentDiscounts.Remove(apartmentDiscount);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateDiscountAsync(Guid id, Discount discount)
        {
            var discountToUpdate = await context.Discounts
                .FirstOrDefaultAsync(x => x.Id == id);
            if (discountToUpdate is null)
                return false;
            discountToUpdate.Description = discount.Description;
            discountToUpdate.Percentage = discount.Percentage;
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteDiscountAsync(Guid id)
        {
            var discount = await context.Discounts
                .Include(discount => discount.ApartmentDiscounts)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (discount is null)
                return false;
            context.ApartmentDiscounts.RemoveRange(discount.ApartmentDiscounts);
            context.Discounts.Remove(discount);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> IsAddedAsync(Guid id)
        {
var discount = await context.Discounts
                .Include(d => d.ApartmentDiscounts)
                .FirstOrDefaultAsync(x => x.Id == id);
            return discount.ApartmentDiscounts.Any();
        }
        public async Task<int> GetNumberOfApartmentsWithDiscountByIdAsync(Guid id)
        {
            var result = await context.Discounts
                .Include(d => d.ApartmentDiscounts)
                .Where(d => d.Id == id)
                .SelectMany(d => d.ApartmentDiscounts)
                .CountAsync();
            return result;
        }
    }
}

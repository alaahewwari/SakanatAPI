using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace BusinessLogic.Repositories.Implementations
{
    public class FavouritesRepository(
        ApplicationDbContext context,
        ISieveProcessor sieveProcessor,
        IMapper mapper) 
        : IFavouritesRepository
    {
        public async Task<IList<ApartmentOverviewResponseDto>> GetAllFavourites(Guid userId, SieveModel sieveModel)
        {
            var apartmentsQuery = context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.FavouriteApartments)
                .AsNoTracking();
            var paginatedApartments = sieveProcessor.Apply(sieveModel, apartmentsQuery);
            var projectedApartments = await paginatedApartments
                .ProjectTo<ApartmentOverviewResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return projectedApartments;

        }

        public async Task<Apartment?> GetFavouriteApartmentByIdAsync(Guid apartmentId, Guid userId)
        {
             var apartment= await context.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.FavouriteApartments)
            .FirstOrDefaultAsync(a => a.Id == apartmentId);
            return apartment;
        }
        public async Task<bool> AddApartmentToFavouritesAsync(Guid apartmentId, Guid userId)
        {
           var user =await context.Users
            .Include(u => u.FavouriteApartments)
            .FirstOrDefaultAsync(u => u.Id == userId);

            var apartment =await context.Apartments
            .Where(a=>a.IsAvailable==true)
            .FirstOrDefaultAsync(a => a.Id == apartmentId);

            if (user is null || apartment is null)
                return false;

            user.FavouriteApartments.Add(apartment);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFavouriteApartment(Guid apartmentId, Guid userId)
        {
             var user = await context.Users
            .Include(u => u.FavouriteApartments)
            .FirstOrDefaultAsync(u => u.Id == userId);

            var apartment = await context.Apartments
            .FirstOrDefaultAsync(a => a.Id == apartmentId);
            if (user is null || apartment is null)
                return false;

            user.FavouriteApartments.Remove(apartment);
            await context.SaveChangesAsync();
            return true;
        }
    }
}

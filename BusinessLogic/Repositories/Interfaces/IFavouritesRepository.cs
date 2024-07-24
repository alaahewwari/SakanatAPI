using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using DataAccess.Models;
using Sieve.Models;
namespace BusinessLogic.Repositories.Interfaces
{
    public interface IFavouritesRepository
    {
        Task<IList<ApartmentOverviewResponseDto>> GetAllFavourites(Guid userId,SieveModel sieveModel);
        Task<Apartment?> GetFavouriteApartmentByIdAsync(Guid apartmentId, Guid userId);
        Task<bool> AddApartmentToFavouritesAsync(Guid apartmentId, Guid userId);
        Task<bool> RemoveFavouriteApartment(Guid apartmentId, Guid userId);
    }
}

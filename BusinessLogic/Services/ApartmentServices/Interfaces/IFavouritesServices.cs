using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.ApartmentServices.Interfaces
{
    public interface IFavouritesServices
    {
        Task<ErrorOr<SuccessResponse>> AddToFavouritesAsync(Guid userId, Guid apartmentId);
        Task<ErrorOr<SuccessResponse>> RemoveApartmentFromFavouritesAsync(Guid userId, Guid apartmentId);
        Task<ErrorOr<IList<ApartmentOverviewResponseDto>>> GetAllFavouriteApartmentsAsync(Guid userId,
            SieveModel sieveModel);
        Task<ErrorOr<ApartmentOverviewResponseDto>> GetFavouriteApartmentByIdAsync(Guid userId, Guid apartmentId);
    }
}

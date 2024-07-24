using AutoMapper;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.ApartmentServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.UserServices.Interfaces;
using ErrorOr;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Implementations;
using DataAccess.Models;
using BusinessLogic.Common.Models;
using Sieve.Models;


namespace BusinessLogic.Services.ApartmentServices.Implementations
{
    public class FavouritesServices(
    IFavouritesRepository favouritesRepository,
    IApartmentRepository apartmentRepository,
    IIdentityManager identityManager,
    IMapper mapper)
        : IFavouritesServices
    {
        public async Task<ErrorOr<SuccessResponse>> AddToFavouritesAsync(Guid userId, Guid apartmentId)
        {
            var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
            if (apartment is null)
                return Errors.Apartment.ApartmentNotFound;

            var user = await identityManager.FindByIdAsync(userId);
            if (user is null)
                return Errors.Identity.UserNotFound;

            var isApartmentAlreadyFavourite =await favouritesRepository.GetFavouriteApartmentByIdAsync(apartmentId,userId);

            if (isApartmentAlreadyFavourite is not null)
                return Errors.Apartment.ApartmentAlreadyFavourite;

            var isAdded = await favouritesRepository.AddApartmentToFavouritesAsync(apartmentId, userId);
            if (!isAdded)
                return Errors.Unknown.Create("Failed to add apartment to favourites");

            return new SuccessResponse("Apartment added to favourites successfully");

        }

        public async Task<ErrorOr<IList<ApartmentOverviewResponseDto>>> GetAllFavouriteApartmentsAsync(Guid userId,SieveModel sieveModel)
        {
            var user = await identityManager.FindByIdAsync(userId);
            if (user is null)
                return Errors.Identity.UserNotFound;

            var apartments = await favouritesRepository.GetAllFavourites(userId,sieveModel);
            return apartments.ToList();
        }

        public async Task<ErrorOr<ApartmentOverviewResponseDto>> GetFavouriteApartmentByIdAsync(Guid userId, Guid apartmentId)
        {
            var apartment =await favouritesRepository.GetFavouriteApartmentByIdAsync(apartmentId, userId);
            if (apartment is null)
                return Errors.Apartment.ApartmentNotFound;

            var response = mapper.Map<ApartmentOverviewResponseDto>(apartment);
            if (response is null)
                return Errors.Apartment.NoApartmentsFound;

            return response;
        }

        public async Task<ErrorOr<SuccessResponse>> RemoveApartmentFromFavouritesAsync(Guid userId, Guid apartmentId)
        {
            var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
            if (apartment is null)
                return Errors.Apartment.ApartmentNotFound;

            var user = await identityManager.FindByIdAsync(userId);
            if (user is null)
                return Errors.Identity.UserNotFound;

            var isApartmentAlreadyFavourite =await favouritesRepository.GetFavouriteApartmentByIdAsync(apartmentId, userId);

            if (isApartmentAlreadyFavourite is null)
                return Errors.Apartment.ApartmentNotFound;

            var isRemoved = await favouritesRepository.RemoveFavouriteApartment(apartmentId, userId);
            if (!isRemoved)
                return Errors.Unknown.Create("Failed to remove apartment from favourites");

            return new SuccessResponse("Apartment removed from favourites successfully");
        }
    }
}

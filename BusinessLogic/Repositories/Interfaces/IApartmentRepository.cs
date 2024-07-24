using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces;

public interface IApartmentRepository
{
    Task<PagedResult<ApartmentOverviewResponseDto>> GetAllApartmentsAsync(SieveModel sieveModel);
    Task<Apartment?> CreateApartmentAsync(Apartment? apartment, ApplicationUser user);
    Task<ApartmentResponseDto?> GetApartmentByIdAsync(Guid id);
    Task<IList<ApartmentResponseDto>?> GetApartmentsByUserId(Guid id, SieveModel sieveModel);
    Task<Apartment> UpdateApartmentAsync(Guid id, Apartment apartment);
    Task<Apartment?> DeleteApartmentAsync(Guid id);
    Task<IList<ApartmentImage>?> UploadApartmentImagesAsync(List<string> images, Guid id);
    Task<ApartmentImageResponseDto> AddApartmentImageAsync(ApartmentImage image);
    Task<IList<ApartmentImage>?> GetApartmentImagesAsync(Guid apartmentId);
    Task<ApartmentImage?> GetApartmentImageByIdAsync(Guid imageId);
    Task<bool> UpdateApartmentImageAsync(ApartmentImage image);
    Task<bool> DeleteApartmentImageAsync(Guid apartmentId, Guid imageId);
    Task<IList<ApartmentDiscountsResponseDto?>> GetAllDiscountsForApartmentsByUserIdAsync(Guid userId);
    Task<int> GetActiveApartmentWithDiscountsNumberAsync();
    Task<bool> ChangeApartmentAvailabilityAsync(Guid id);
    Task<bool> ChangeApartmentVisibilityAsync(Guid id);

}
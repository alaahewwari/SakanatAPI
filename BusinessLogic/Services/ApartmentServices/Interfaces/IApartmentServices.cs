using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ApartmentDtos.Requests;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Sieve.Models;

namespace BusinessLogic.Services.ApartmentServices.Interfaces;

public interface IApartmentServices
{
    Task<ErrorOr<PagedResult<ApartmentOverviewResponseDto>>> GetAllApartments(SieveModel sieveModel);
    Task<ErrorOr<CreateApartmentResponseDto>> CreateApartmentAsync(ApartmentRequestDto apartmentDto);
    Task<ErrorOr<ApartmentResponseDto>> GetApartmentByIdAsync(Guid id);
    Task<ErrorOr<IList<ApartmentResponseDto>>> GetApartmentsByUserId(Guid id, SieveModel sieveModel);
    Task<ErrorOr<SuccessResponse>> UpdateApartmentAsync(Guid id, ApartmentToUpdateRequestDto request);
    Task<ErrorOr<SuccessResponse>> DeleteApartmentAsync(Guid id);
    Task<ErrorOr<IList<ApartmentImageResponseDto>>> UploadImagesAsync(List<IFormFile> images, Guid apartmentId);
    Task<ErrorOr<ApartmentImageResponseDto>> UpdateApartmentImageAsync(Guid apartmentId, Guid ImageId, IFormFile file);
    Task<ErrorOr<IList<ApartmentImageResponseDto>>> GetApartmentImagesAsync(Guid id);
    Task<ErrorOr<ApartmentImageResponseDto>> GetApartmentImageByIdAsync(Guid imageId);
    Task<ErrorOr<SuccessResponse>> DeleteApartmentImageAsync(Guid apartmentId, Guid imageId);
    Task<ErrorOr<IList<ApartmentDiscountsResponseDto>>> GetUserApartmentDiscounts(Guid userId);
    Task<ApartmentWithDiscountsNumberResponseDto> GetActiveApartmentWithDiscountsNumberAsync();
    Task<ErrorOr<SuccessResponse>> ChangeApartmentAvailabilityAsync(Guid apartmentId);
    Task<ErrorOr<SuccessResponse>> ChangeApartmentVisibilityAsync(Guid apartmentId);
}
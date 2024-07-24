using BusinessLogic.Contracts.General;
using BusinessLogic.Dtos.University;
using BusinessLogic.DTOs.UniversityDtos.Requests;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.UniversityServices.Interfaces;

public interface IUniversityServices
{
    Task<ErrorOr<IList<UniversityResponseDto>>> GetAllUniversityAsync(SieveModel sieveModel);
    Task<ErrorOr<UniversityResponseDto>> GetUniversityByIdAsync(Guid id);
    Task<ErrorOr<SuccessResponse>> CreateUniversityAsync(Guid cityId, UniversityRequestDto university);
    Task<ErrorOr<SuccessResponse>> UpdateUniversityAsync(Guid id, UniversityRequestDto university);
    Task<ErrorOr<IList<UniversityResponseDto>>> GetUniversitiesByCityIdAsync(Guid cityId);
}
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.CityDtos.Requests;
using BusinessLogic.DTOs.CityDtos.Responses;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.CityServices.Interfaces;

public interface ICityServices
{
    Task<ErrorOr<List<CityResponseDto>>> GetAllCitiesAsync(SieveModel sieveModel);
    Task<ErrorOr<CityResponseDto>> GetCityByNameAsync(string name);
    Task<ErrorOr<CityResponseDto>> GetCityByIdAsync(Guid id);
    Task<ErrorOr<SuccessResponse>> CreateCityAsync(CityRequestDto request);
    Task<ErrorOr<SuccessResponse>> UpdateCityAsync(Guid id, CityRequestDto request);
}
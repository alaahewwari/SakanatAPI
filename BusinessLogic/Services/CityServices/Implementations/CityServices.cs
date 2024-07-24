using AutoMapper;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.CityDtos.Requests;
using BusinessLogic.DTOs.CityDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.CityServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.CityServices.Implementations;

public class CityServices(ICityRepository cityRepository, IMapper mapper) : ICityServices
{
    public async Task<ErrorOr<List<CityResponseDto>>> GetAllCitiesAsync(SieveModel sieveModel)
    {
        var cities = await cityRepository.GetAllCitiesAsync(sieveModel);
        var response = mapper.Map<List<CityResponseDto>>(cities);
        return response;
    }

    public async Task<ErrorOr<CityResponseDto>> GetCityByIdAsync(Guid id)
    {
        var city = await cityRepository.GetCityByIdAsync(id);
        var response = mapper.Map<CityResponseDto>(city);
        return response;
    }

    public async Task<ErrorOr<CityResponseDto>> GetCityByNameAsync(string name)
    {
        var city = await cityRepository.GetCityByNameAsync(name);
        var response = mapper.Map<CityResponseDto>(city);
        if (city is null)
            return Errors.City.CityNotFound;

        return response;
    }

    public async Task<ErrorOr<SuccessResponse>> CreateCityAsync(CityRequestDto cityDto)
    {
        var cityExist = await cityRepository.GetCityByNameAsync(cityDto.Name);
        if (cityExist != null)
        {
            return Errors.City.CityAlreadyExists;
        }
        var city = mapper.Map<City>(cityDto);
        await cityRepository.CreateCityAsync(city);
        return new SuccessResponse("City created successfully");
    }

    public async Task<ErrorOr<SuccessResponse>> UpdateCityAsync(Guid id, CityRequestDto request)
    {
        var city = await cityRepository.GetCityByIdAsync(id);
        if (city is null)
        {
            return Errors.City.CityNotFound;
        }
        var cityExist = await cityRepository.GetCityByNameAsync(request.Name);
        if (cityExist != null)
        {
            return Errors.City.CityAlreadyExists;
        }
        var result = mapper.Map<City>(request);
        await cityRepository.UpdateCityAsync(id, result);
        return new SuccessResponse("City updated successfully");
    }
}
using AutoMapper;
using BusinessLogic.Contracts.General;
using BusinessLogic.Dtos.University;
using BusinessLogic.DTOs.UniversityDtos.Requests;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.UniversityServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.UniversityServices.Implementations;

public class UniversityServices(
    IUniversityRepository universityRepository,
    ICityRepository cityRepository,
    IMapper mapper)
    : IUniversityServices
{
    public async Task<ErrorOr<IList<UniversityResponseDto>>> GetAllUniversityAsync(SieveModel sieveModel)
    {
        var universities = await universityRepository.GetAllUniversityAsync(sieveModel);
        var universitiesDto = mapper.Map<List<UniversityResponseDto>>(universities);
        if (universities is null)
        {
            return Errors.University.NoUniversitiesFound;
        }
        return universitiesDto;
    }

    public async Task<ErrorOr<UniversityResponseDto>> GetUniversityByIdAsync(Guid id)
    {
        var university = await universityRepository.GetUniversityByIdAsync(id);
        var universityDto = mapper.Map<UniversityResponseDto>(university);
        if (university is null) return Errors.University.UniversityNotFound;
        return universityDto;
    }

    public async Task<ErrorOr<SuccessResponse>> CreateUniversityAsync(Guid cityId, UniversityRequestDto university)
    {
        var city = await cityRepository.GetCityByIdAsync(cityId);
        if (city is null)
        {
            return Errors.City.CityNotFound;
        }
        var univ = await universityRepository.GetUniversityByNameAsync(university.Name);
        var universities = await universityRepository.GetUniversitiesByCityIdAsync(city.Id);
        if (universities.Any(u => u.Name == university.Name))
        {
            return Errors.University.UniversityAlreadyExist;
        }
        var universityModel = mapper.Map<University>(university);
        await universityRepository.CreateUniversityAsync(cityId, universityModel);
        return new SuccessResponse("University created successfully");
    }

    public async Task<ErrorOr<SuccessResponse>> UpdateUniversityAsync(Guid id,UniversityRequestDto universityDto)
    {
        var university = await universityRepository.GetUniversityByIdAsync(id);
        if (university is null)
        {
            return Errors.University.UniversityNotFound;
        }
        var updatedUniversity = mapper.Map<University>(universityDto);
        await universityRepository.UpdateUniversityAsync(id, updatedUniversity);
        return new SuccessResponse("University updated successfully");
    }


    public async Task<ErrorOr<IList<UniversityResponseDto>>> GetUniversitiesByCityIdAsync(Guid cityId)
    {
        var universities = await universityRepository.GetUniversitiesByCityIdAsync(cityId);
        var universitiesDto = mapper.Map<List<UniversityResponseDto>>(universities);
        if (universities.Count == 0)
        {
            return Errors.University.NoUniversitiesFound;
        }
        return universitiesDto;
    }
}
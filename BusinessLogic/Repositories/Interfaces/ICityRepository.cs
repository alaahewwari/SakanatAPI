using BusinessLogic.DTOs.CityDtos.Responses;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces;

public interface ICityRepository
{
    Task<IList<CityResponseDto>> GetAllCitiesAsync(SieveModel sieveModel);
    Task<City?> GetCityByNameAsync(string name);
    Task<City?> GetCityByIdAsync(Guid id);
    Task CreateCityAsync(City city);
    Task UpdateCityAsync(Guid id, City city);
}
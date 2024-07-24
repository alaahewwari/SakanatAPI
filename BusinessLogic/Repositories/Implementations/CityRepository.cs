using AutoMapper.QueryableExtensions;
using AutoMapper;
using BusinessLogic.Dtos.University;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using BusinessLogic.DTOs.CityDtos.Responses;

namespace BusinessLogic.Repositories.Implementations;

public class CityRepository(
    ApplicationDbContext context,
    ISieveProcessor sieveProcessor,
    IMapper mapper
) : ICityRepository
{
    public async Task CreateCityAsync(City city)
    {
        city.CreationDate = DateOnly.FromDateTime(DateTime.Now);
        await context.Cities.AddAsync(city);
        await context.SaveChangesAsync();
    }

    public async Task<IList<CityResponseDto>> GetAllCitiesAsync(SieveModel sieveModel)
    {
        var citiesQuery = context.Cities.AsNoTracking();
        var result = await sieveProcessor.Apply(sieveModel, citiesQuery)
            .ProjectTo<CityResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return result;
    }

    public async Task<City?> GetCityByIdAsync(Guid id)
    {
        var city=await context.Cities.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return city;
    }

    public async Task<City?> GetCityByNameAsync(string name)
    {
        var city= await context.Cities.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name);
        return city;
    }

    public async Task UpdateCityAsync(Guid id, City city)
    {
       var updatedCity = await context.Cities
            .FirstOrDefaultAsync(c => c.Id == id);
        updatedCity.Name = city.Name;
        await context.SaveChangesAsync();
    }
}
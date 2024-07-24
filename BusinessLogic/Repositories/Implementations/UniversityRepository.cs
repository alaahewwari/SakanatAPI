using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.Dtos.University;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
namespace BusinessLogic.Repositories.Implementations;

public class UniversityRepository(
    ApplicationDbContext context,
    ISieveProcessor sieveProcessor,
    IMapper mapper)
 : IUniversityRepository
{
    public async Task<IList<UniversityResponseDto>?> GetAllUniversityAsync(SieveModel sieveModel)
    {
        var universitiesQuery = context.Universities.AsNoTracking();
        var result = await sieveProcessor.Apply(sieveModel, universitiesQuery)
            .ProjectTo<UniversityResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync(); 
        return result;
    }
    public async Task<University?> GetUniversityByIdAsync(Guid id)
    {
        return await context.Universities.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<University?> GetUniversityByNameAsync(string name)
    {
        return (await context.Universities.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name));
    }

    public async Task CreateUniversityAsync(Guid cityId,University university)
    {
        var city = await context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
        var universities = await context.Universities.ToListAsync();
        if (universities.Any(u => u.Name == university.Name))
        {
            city.ContainedUniversities.Add(university);
        }
        else
        {
            university.CreationDate = DateOnly.FromDateTime(DateTime.Now);
            city!.ContainedUniversities.Add(university);
        }
        await context.SaveChangesAsync();
    }

    public async Task UpdateUniversityAsync(Guid universityId, University updatedUniversity)
    {
        var university = await context.Universities.FirstOrDefaultAsync(c => c.Id == universityId);
                       university.Name = updatedUniversity.Name;
                       await context.SaveChangesAsync();
                   }

    public async Task<List<UniversityResponseDto>> GetUniversitiesByCityIdAsync(Guid cityId)
    {
        var universities = await context.Cities.Include(c => c.ContainedUniversities)
            .Where(c => c.Id == cityId)
            .SelectMany(c => c.ContainedUniversities)
            .ProjectTo<UniversityResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();
        return universities;
    }
}
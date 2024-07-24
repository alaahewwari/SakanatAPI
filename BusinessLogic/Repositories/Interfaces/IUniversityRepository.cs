using BusinessLogic.Dtos.University;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces;

public interface IUniversityRepository
{
    Task<IList<UniversityResponseDto>?> GetAllUniversityAsync(SieveModel sieveModel);
    Task<University?> GetUniversityByIdAsync(Guid id);
    Task<University?> GetUniversityByNameAsync(string name);
    Task CreateUniversityAsync(Guid id,University university);
    Task UpdateUniversityAsync(Guid universityId, University university);
    Task<List<UniversityResponseDto>> GetUniversitiesByCityIdAsync(Guid cityId);

}
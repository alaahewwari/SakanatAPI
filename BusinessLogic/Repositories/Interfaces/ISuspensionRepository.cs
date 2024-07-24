using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces;

public interface ISuspensionRepository
{
    Task CreateSuspensionAsync(Suspension suspension);
    Task UnSuspendUserAsync(Guid userId);
    Task<PagedResult<GetSuspendedUserResponseDto>?> GetAllSuspendedUsersAsync(SieveModel sieveModel);
    Task<Suspension?> GetSuspendedUserByIdAsync(Guid userId);
    Task<IList<string>?> GetSuspensionReasonsAsync();
}
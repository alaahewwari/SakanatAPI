using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<PagedResult<GetUserResponseDto>> GetAllUsersAsync(SieveModel sieveModel, string? fullname);
        Task<ApplicationUser?> FindByEmailAsync(string email);
        Task<ApplicationUser?> FindByRefreshTokenAsync(string refreshToken);
        Task DeleteUserAsync(ApplicationUser user);
        Task<bool> DeleteProfileImageAsync(ApplicationUser user);
        Task<Role> GetUserRoleAsync(ApplicationUser user);
    }
}

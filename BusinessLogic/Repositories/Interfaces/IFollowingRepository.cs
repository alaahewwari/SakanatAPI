using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Models;
using Sieve.Models;
namespace BusinessLogic.Repositories.Interfaces
{
    public interface IFollowingRepository
    {
        Task<bool> FollowUserAsync(ApplicationUser user, ApplicationUser followedUser);
        Task<bool> UnfollowUserAsync(ApplicationUser user, ApplicationUser followedUser);
        Task<IList<UserOverviewResponseDto>> GetFollowersAsync(ApplicationUser user, SieveModel sieveModel);
        Task<IList<UserOverviewResponseDto>> GetFollowingAsync(ApplicationUser user, SieveModel sieveModel);
    }
}

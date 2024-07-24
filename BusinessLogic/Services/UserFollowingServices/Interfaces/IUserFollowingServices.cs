

using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.UserDtos.Responses;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.UserFollowingServices.Interfaces
{
    public interface IUserFollowingServices
    {
        Task<ErrorOr<SuccessResponse>> FollowUserAsync(Guid userId);
        Task<ErrorOr<SuccessResponse>> UnfollowUserAsync(Guid userId);
        Task<ErrorOr<IList<UserOverviewResponseDto>>> GetFollowersAsync(Guid userId, SieveModel sieveModel);
        Task<ErrorOr<IList<UserOverviewResponseDto>>> GetFollowingAsync(Guid userId, SieveModel sieveModel);
    }
}

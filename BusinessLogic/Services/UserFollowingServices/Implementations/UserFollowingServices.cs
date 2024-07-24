
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.NotificationServices.Interfaces;
using BusinessLogic.Services.UserFollowingServices.Interfaces;
using DataAccess.Enums.Notification;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.UserFollowingServices.Implementations
{
    public class UserFollowingServices(
        IFollowingRepository followingRepository,
        IIdentityManager identityManager,
        IUnitOfWork unitOfWork,
        INotificationRepository notificationRepository,
        INotificationServices notificationServices)
        : IUserFollowingServices 
    {
        public async Task<ErrorOr<SuccessResponse>> FollowUserAsync(Guid userId)
        {
            var user = await identityManager.GetLoggedInUserIdAsync();
            if (user is null)
            {
                return Errors.Identity.Unauthorized;
            }
            if (user.Id == userId)
            {
                return Errors.Following.CannotfollowYourself;
            }
            var followedUser = await identityManager.FindByIdAsync(userId);
            if (followedUser is null)
            {
                return Errors.Identity.UserNotFound;
            }
            if(user.FollowingUsers.Any(u => u.FollowingId == userId))
            {
                return Errors.Following.UserAlreadyFollowed;
            }
            try
            {
                await unitOfWork.BeginTransactionAsync();
                var result = await followingRepository.FollowUserAsync(user, followedUser);
                List<Guid> followedUsers = [followedUser.Id];
                await notificationServices.CreateNotificationAsync(NotificationType.NewFollower, user.Id, followedUsers, null);
                await unitOfWork.CommitTransactionAsync();
                return new SuccessResponse("User followed successfully");
            }
            catch (Exception e)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Errors.Following.FollowingFailed;
            }
        }
        public async Task<ErrorOr<SuccessResponse>> UnfollowUserAsync(Guid userId)
        {
            var user = await identityManager.GetLoggedInUserIdAsync();

            if (user is null)
            {
                return Errors.Identity.Unauthorized;
            }

            if (user.Id == userId)
            {
                return Errors.Following.CannotUnfollowYourself;
            }
            var unfollowedUser = await identityManager.FindByIdAsync(userId);
            if (unfollowedUser is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var result = await followingRepository.UnfollowUserAsync(user, unfollowedUser);
            if (!result) { 
                return Errors.Following.UnfollowingFailed;
            }
            var notification = await notificationRepository.GetNotificationByTypeAsync(NotificationType.NewFollower, user.Id, unfollowedUser.Id);
            if (notification is not null)
            {
                await notificationRepository.DeleteNotificationAsync(notification.Id);
            }
            return new SuccessResponse("User unfollowed successfully");
        }
        public async Task<ErrorOr<IList<UserOverviewResponseDto>>> GetFollowersAsync(Guid userId, SieveModel sieveModel)
        {
            var user = await identityManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var followers = await followingRepository.GetFollowersAsync(user, sieveModel);
            return followers.ToList();
        }
        public async Task<ErrorOr<IList<UserOverviewResponseDto>>> GetFollowingAsync(Guid userId, SieveModel sieveModel)
        {
            var user = await identityManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var following = await followingRepository.GetFollowingAsync(user,sieveModel);
            return following.ToList();
        }

    }
}

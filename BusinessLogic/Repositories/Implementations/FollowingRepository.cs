using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.ContractDtos.Responses;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace BusinessLogic.Repositories.Implementations
{
    public class FollowingRepository(
        ApplicationDbContext context,
        ISieveProcessor sieveProcessor,
        IMapper mapper
        )
        : IFollowingRepository
    {
        public async Task<bool> FollowUserAsync(ApplicationUser user,ApplicationUser followedUser)
        {
            var follow = new UserFollows
            {
                FollowerId = user.Id,
                FollowingId = followedUser.Id
            };
            await context.Followings.AddAsync(follow);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UnfollowUserAsync(ApplicationUser user, ApplicationUser? followedUser)
        {
            var follow = await context.Followings
                .FirstOrDefaultAsync(f => f.FollowerId == user.Id && f.FollowingId == followedUser.Id);
            if (follow is null)
            {
                return false;
            }
            context.Followings.Remove(follow);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<IList<UserOverviewResponseDto>> GetFollowersAsync(ApplicationUser user, SieveModel sieveModel)
        {

            var followers = await context.Followings
                .AsNoTracking()
                .Where(u => u.FollowingId == user.Id)
                .Select(u => u.Follower)
                .ProjectTo<UserOverviewResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return followers;

        }
        public async Task<IList<UserOverviewResponseDto>> GetFollowingAsync(ApplicationUser user, SieveModel sieveModel)
        {

            var followings =await context.Followings
                .AsNoTracking()
                .Where(u => u.FollowerId == user.Id)
                .Select(u => u.Following)
                .ProjectTo<UserOverviewResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return followings;
        }
    }
}

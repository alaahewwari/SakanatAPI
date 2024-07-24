using BusinessLogic.Services.UserFollowingServices.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Text.Json;
namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserFollowingController(IUserFollowingServices userFollowingServices) : ApiController
    {
        [HttpPost]
        [Route(ApiRoutes.UserFollowing.FollowOwner)]
        public async Task<IActionResult> FollowUserAsync(Guid ownerId)
        {
            var response = await userFollowingServices.FollowUserAsync(ownerId);
            return response.Match(
                            Ok,
                            Problem
                                                                                                                                                   );
        }
        [HttpPost]
        [Route(ApiRoutes.UserFollowing.UnfollowOwner)]
        public async Task<IActionResult> UnfollowUserAsync(Guid ownerId)
        {
            var response = await userFollowingServices.UnfollowUserAsync(ownerId);
            return response.Match(
                Ok,
                Problem
                );
        }
        [HttpGet]
        [Route(ApiRoutes.UserFollowing.GetFollowers)]
        public async Task<IActionResult> GetFollowersAsync([FromRoute] Guid ownerId , [FromQuery] SieveModel sieveModel)
        {
            var response = await userFollowingServices.GetFollowersAsync(ownerId,sieveModel);
            return response.Match(
               Ok,
               Problem
           ); 
        }
        [HttpGet]
        [Route(ApiRoutes.UserFollowing.GetFollowing)]
        public async Task<IActionResult> GetFollowingAsync([FromRoute] Guid ownerId,[FromQuery] SieveModel sieveModel)
        {
            var response = await userFollowingServices.GetFollowingAsync(ownerId, sieveModel);
            return response.Match(
               Ok,
               Problem
           );
        }
    }
}

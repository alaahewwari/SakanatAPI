using BusinessLogic.Services.ApartmentServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers
{
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class FavouritesController(IFavouritesServices favouritesServices) : ApiController
    {
        [HttpPost]
        [Route(ApiRoutes.Favourites.AddToFavourites)]
        [Authorize(Roles = "Owner, Customer")]
        public async Task<IActionResult> AddToFavouritesAsync(Guid userId, Guid apartmentId)
        {
            var response = await favouritesServices.AddToFavouritesAsync(userId, apartmentId);
            return response.Match(
                               Ok,
                               Problem
                                 );
        }
        [HttpPost]
        [Route(ApiRoutes.Favourites.RemoveFromFavourites)]
        [AllowAnonymous]
        [Authorize(Roles = "Owner, Customer")]
        public async Task<IActionResult> RemoveFromFavouritesAsync(Guid userId, Guid apartmentId)
        {
            var response = await favouritesServices.RemoveApartmentFromFavouritesAsync(userId, apartmentId);
            return response.Match(
                                          Ok,
                                                                     Problem
                                                                                                     );
        }
        [HttpGet]
        [Route(ApiRoutes.Favourites.GetFavouritesByUserId)]
        [Authorize(Roles = "Owner, Customer")]
        public async Task<IActionResult> GetFavouritesByUserId([FromQuery] SieveModel sieveModel,Guid userId)
        {
            var response = await favouritesServices.GetAllFavouriteApartmentsAsync(userId,sieveModel);
            return response.Match(
                         Ok,
                                        Problem
                                                       );
        }
        [HttpGet]
        [Route(ApiRoutes.Favourites.GetFavouriteById)]
        [Authorize(Roles = "Owner, Customer")]
        public async Task<IActionResult> GetFavouriteByIdAsync([FromRoute] Guid userId, [FromRoute] Guid apartmentId)
        {
            var response = await favouritesServices.GetFavouriteApartmentByIdAsync(userId, apartmentId);
            return response.Match(
                Ok,
                Problem
                );
        }
    }
}

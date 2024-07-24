using System.Text.Json;
using BusinessLogic.DTOs.DiscountDtos.Requests;
using BusinessLogic.Services.DiscountsServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController 
        (IDiscountServices discountServices)
        : ApiController
    {
        [HttpGet]
        [Route(ApiRoutes.Discounts.GetDiscountsByApartmentId)]
        public async Task <IActionResult> GetAllDiscountsByApartmentId([FromQuery] SieveModel sieveModel,[FromRoute] Guid apartmentId)
        {
var response = await discountServices.GetAllDiscountsByApartmentIdAsync(sieveModel, apartmentId);
            return response.Match(
                               Ok,
                                              Problem
                                              );
        }

        [HttpGet]
        [Route(ApiRoutes.Discounts.GetDiscountsForUser)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetAllDiscountsByUserId([FromQuery] SieveModel sieveModel,[FromRoute] Guid userId)
        {
            var response = await discountServices.GetAllDiscountsForUserAsync(userId, sieveModel);
            return response.Match(
                data =>
                {
                    var result = data;
                    var items = result.Items;
                    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
                    return Ok(items);
                },
                Problem
            ); ;
        }

        [HttpGet]
        [Route(ApiRoutes.Discounts.GetDiscountById)]
        public async Task<IActionResult> GetDiscountById([FromRoute] Guid id)
        {
            var response = await discountServices.GetDiscountById(id);
            return response.Match(
                               Ok,
                                              Problem
                                              );
        }
        [HttpPost]
        [Route(ApiRoutes.Discounts.CreateDiscount)]
        [Authorize(Roles="Owner")]
        public async Task<IActionResult> CreateDiscount([FromBody] DiscountRequestDto request)
        {
            var response = await discountServices.CreateDiscountAsync(request);
            return response.Match(
                                              Ok,
                                                                                           Problem
                                                                                           );
        }

        [HttpPost]
        [Route(ApiRoutes.Discounts.AddDiscountToApartment)]
        [Authorize(Roles="Owner")]
        public async Task<IActionResult> AddDiscountForApartment([FromRoute] Guid apartmentId, [FromRoute] Guid id, DateOnly expiresAt)
        {
            var response = await discountServices.AddDiscountForApartmentAsync(apartmentId, id ,expiresAt);
            return response.Match(
                                                             Ok,
                                                                                                                                                       Problem
                                                                                                                                                       );
        }

        [HttpPut]
        [Route(ApiRoutes.Discounts.UpdateDiscount)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateDiscount([FromRoute] Guid id, [FromBody] DiscountRequestDto request)
        {
            var response = await discountServices.UpdateDiscountAsync(id, request);
            return response.Match(
                                                                            Ok,
                                                                                                                                                                                                                                  Problem
                                                                                                                                                                                                                                                                                                                                                                                        );
        }

            [HttpDelete]
            [Route(ApiRoutes.Discounts.RemoveDiscountFromApartment)]
            [Authorize(Roles="Owner")]
            public async Task<IActionResult> RemoveDiscountFromApartment([FromRoute] Guid apartmentId, [FromRoute] Guid id)
            {
                var response = await discountServices.RemoveDiscountFromApartmentAsync(apartmentId, id);
                return response.Match(
                                                                                      Ok,
                                                                                                                                                                                                                                                  Problem
                                                                                                                                                                                                                                                                                                                                                                                                              );
            }


        [HttpDelete]
        [Route(ApiRoutes.Discounts.DeleteDiscount)]
        [Authorize(Roles="Owner")]
        public async Task<IActionResult> DeleteDiscountAsync(Guid id)
        {
            var response = await discountServices.DeleteDiscountAsync(id);
            return response.Match(
                                                             Ok,
                                                                                                                                                       Problem
                                                                                                                                                       );
        }
    }
}

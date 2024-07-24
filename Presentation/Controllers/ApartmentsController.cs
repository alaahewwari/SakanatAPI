using System.Text.Json;
using BusinessLogic.DTOs.ApartmentDtos.Requests;
using BusinessLogic.Services.ApartmentServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
namespace Presentation.Controllers;
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class ApartmentsController(
    IApartmentServices apartmentServices
    ) : ApiController
{
    [HttpPost]
    [Route(ApiRoutes.Apartments.CreateApartment)]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> CreateNewApartment([FromBody] ApartmentRequestDto apartment)
    {
        var response = await apartmentServices.CreateApartmentAsync(apartment);
        return response.Match(
            value => CreatedAtAction(nameof(GetApartmentById), new { id = value.Id }, value),
            Problem
        );
    }

    [HttpGet]
    [Route(ApiRoutes.Apartments.GetAllApartments)]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllApartments([FromQuery] SieveModel sieveModel)
    {
        var response = await apartmentServices.GetAllApartments(sieveModel);
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
    [Route(ApiRoutes.Apartments.GetApartmentById)]
    public async Task<IActionResult> GetApartmentById([FromRoute] Guid id)
    {
        var response = await apartmentServices.GetApartmentByIdAsync(id);
        return response.Match(
                       Ok,
                                  Problem
                                             );

    }

    [HttpGet]
    [Route(ApiRoutes.Apartments.GetApartmentsByUserId)]
    public async Task<IActionResult> GetApartmentsByUserId([FromRoute] Guid userId,[FromQuery] SieveModel sieveModel)
    {
        var response = await apartmentServices.GetApartmentsByUserId(userId, sieveModel);
        return response.Match(
                                  Ok,
                                  Problem
                              );
    }

    [HttpPut]
    [Route(ApiRoutes.Apartments.UpdateApartment)]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> UpdateApartment([FromRoute] Guid id, [FromBody] ApartmentToUpdateRequestDto request)
    {
        var response = await apartmentServices.UpdateApartmentAsync(id, request);
        return response.Match(
                       Ok,
                       Problem
                                             );
    }

    [HttpDelete]
    [Route(ApiRoutes.Apartments.DeleteApartment)]
    public async Task<IActionResult> DeleteApartment([FromRoute] Guid id)
    {
        var response = await apartmentServices.DeleteApartmentAsync(id);
        return response.Match(
                Ok,
                Problem
                        );
    }
    [HttpPost]
    [Route(ApiRoutes.Apartments.UploadApartmentImages)]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> UploadImages([FromRoute] Guid id, [FromForm] List<IFormFile> images)
    {
        var response = await apartmentServices.UploadImagesAsync(images, id);
        return response.Match(
            Ok,
            Problem
        );
    }
    [HttpPut]
    [Route(ApiRoutes.Apartments.UpdateApartmentImage)]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> UpdateApartmentImage([FromRoute] Guid id, [FromRoute] Guid imageId, IFormFile image)
    {
        var response = await apartmentServices.UpdateApartmentImageAsync(id, imageId, image);
        return response.Match(
                       Ok,
                       Problem
                       );
    }

    [HttpGet]
    [Route(ApiRoutes.Apartments.GetApartmentImages)]
    public async Task<IActionResult> GetApartmentImages([FromRoute] Guid id)
    {
        var response = await apartmentServices.GetApartmentImagesAsync(id);
        return response.Match(
                          Ok,
                          Problem
                          );
    }

    [HttpGet]
    [Route(ApiRoutes.Apartments.GetApartmentImageById)]
    public async Task<IActionResult> GetApartmentImageById([FromRoute] Guid imageId)
    {
        var response = await apartmentServices.GetApartmentImageByIdAsync(imageId);
        return response.Match(
                              Ok,
                              Problem
                              );
    }
    [HttpDelete]
    [Route(ApiRoutes.Apartments.DeleteApartmentImage)]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> DeleteApartmentImage([FromRoute] Guid id,[FromRoute] Guid imageId)
    {
        var response = await apartmentServices.DeleteApartmentImageAsync(id, imageId);
        return response.Match(
                            Ok,
                            Problem
                            );
    }


    [HttpGet]
    [Route(ApiRoutes.Apartments.GetUserApartmentDiscounts)]
    public async Task<IActionResult> GetUserApartmentDiscounts([FromRoute] Guid userId)
    {
        var response = await apartmentServices.GetUserApartmentDiscounts(userId);
        return response.Match(
                                  Ok,
                                  Problem
                             );
    }


    [HttpGet]
    [Route(ApiRoutes.Apartments.GetActiveApartmentWithDiscountsNumber)]
    public async Task<IActionResult> GetActiveApartmentWithDiscountsNumber()
    {
        var response = await apartmentServices.GetActiveApartmentWithDiscountsNumberAsync();
        return Ok(response);
    }

    [HttpPut]
    [Route(ApiRoutes.Apartments.Availability)]
    [Authorize(Roles ="Owner")]
    public async Task<IActionResult> ChangeApartmentAvailability([FromRoute] Guid id)
    {
        var response = await apartmentServices.ChangeApartmentAvailabilityAsync(id);
        return response.Match(
                       Ok,
                                  Problem
                                         );
    }

    [HttpPut]
    [Route(ApiRoutes.Apartments.Visibility)]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> ChangeApartmentVisibility( [FromRoute] Guid id)
    {
        var response = await apartmentServices.ChangeApartmentVisibilityAsync(id);
        return response.Match(
                                  Ok,
                                                                   Problem
                                                                                                           );
    }
}
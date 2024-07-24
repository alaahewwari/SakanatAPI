using BusinessLogic.DTOs.CityDtos.Requests;
using BusinessLogic.Services.CityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class CitiesController(ICityServices cityServices) : ApiController
{
    [HttpGet]
    [Route(ApiRoutes.Cities.GetAllCities)]
    public async Task<IActionResult> GetAllCitiesAsync([FromQuery] SieveModel sieveModel)
    {
        var response = await cityServices.GetAllCitiesAsync(sieveModel);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    [Route(ApiRoutes.Cities.GetCityById)]
    public async Task<IActionResult> GetCityByIdAsync(Guid id)
    {
        var response = await cityServices.GetCityByIdAsync(id);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPost]
    [Route(ApiRoutes.Cities.CreateCity)]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> CreateCityAsync([FromBody] CityRequestDto request)
    {
        var response = await cityServices.CreateCityAsync(request);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPut]
    [Route(ApiRoutes.Cities.UpdateCity)]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> UpdateCityAsync(Guid id, [FromBody] CityRequestDto request)
    {
        var response = await cityServices.UpdateCityAsync(id, request);
        return response.Match(
            Ok,
            Problem
        );
    }
}
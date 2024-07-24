using BusinessLogic.DTOs.UniversityDtos.Requests;
using BusinessLogic.Services.UniversityServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers;

[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class UniversitiesController(IUniversityServices universityServices) : ApiController
{
    [HttpGet]
    [Route(ApiRoutes.Universities.GetAllUniversities)]
    public async Task<IActionResult> GetAllUniversityAsync([FromQuery] SieveModel sieveModel)
    {
        var response = await universityServices.GetAllUniversityAsync(sieveModel);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    [Route(ApiRoutes.Universities.GetUniversityById)]
    public async Task<IActionResult> GetUniversityByIdAsync(Guid id)
    {
        var response = await universityServices.GetUniversityByIdAsync(id);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPost]
    [Route(ApiRoutes.Universities.CreateUniversity)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateUniversityAsync(Guid cityId, [FromBody] UniversityRequestDto request)
    {
        var response = await universityServices.CreateUniversityAsync(cityId, request);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPut]
    [Route(ApiRoutes.Universities.UpdateUniversity)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateUniversityAsync(Guid id,[FromBody] UniversityRequestDto request)
    {
        var response = await universityServices.UpdateUniversityAsync(id, request);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    [Route(ApiRoutes.Universities.GetUniversitiesByCityId)]
    public async Task<IActionResult> GetUniversitiesByCityIdAsync(Guid cityId)
    {
        var response = await universityServices.GetUniversitiesByCityIdAsync(cityId);
        return response.Match(
                       Ok,
                       Problem
                        );
    }
}
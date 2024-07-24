using System.Text.Json;
using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
namespace Presentation.Controllers;
[Route(ApiRoutes.BaseRoute)]
[ApiController]
[Authorize]
public class SuspensionsController(ISuspensionServices suspensionServices) : ApiController
{
    [HttpPost]
    [Route(ApiRoutes.Suspensions.CreateSuspension)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> SuspendUserAsync(SuspendUserRequestDto request)
    {
        var response = await suspensionServices.SuspendUserAsync(request);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPost]
    [Route(ApiRoutes.Suspensions.DeleteSuspension)]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> UnSuspendUserAsync(Guid userId)
    {
        var response = await suspensionServices.UnSuspendUserAsync(userId);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    [Route(ApiRoutes.Suspensions.GetAllSuspensions)]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllSuspendedUsersAsync([FromQuery] SieveModel sieveModel)
    {
        var response = await suspensionServices.GetAllSuspendedUsersAsync(sieveModel);
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
    [Route(ApiRoutes.Suspensions.GetSuspendedUserById)]
    [Authorize(Roles = "Admin,Owner")]
    public async Task<IActionResult> GetSuspendedUserByIdAsync([FromRoute] Guid userId)
    {
        var response = await suspensionServices.GetSuspendedUserByIdAsync(userId);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpGet]
    [Route(ApiRoutes.Suspensions.GetAllSuspensionReasons)]
    [Authorize(Roles="Admin")]
    public async Task<IActionResult> GetAllSuspensionReasons()
    {
        var response = await suspensionServices.GetAllSuspensionReasonsAsync();
        return response.Match(
            Ok,
            Problem
        );
    }
}
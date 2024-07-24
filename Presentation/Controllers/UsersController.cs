using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.Services.UserServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;
using System.Text.Json;

namespace Presentation.Controllers;
[Route(ApiRoutes.BaseRoute)]
[ApiController]
public class UsersController(
    IUserServices userServices
    ) : ApiController
{
    [HttpGet]
    [Route(ApiRoutes.Users.GetAllUsers)]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] SieveModel sieveModel,string? FullName)
    {
        var response = await userServices.GetAllUsersAsync(sieveModel, FullName);
        return response.Match(
            data =>
            {
                var result = data;
                var items = result.Items;
                Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
                return Ok(items);
            },
            Problem
        );;
    }

    [HttpGet]
    [Route(ApiRoutes.Users.GetUserById)]
    public async Task<IActionResult> GetUserByIdAsync( Guid id)
    {
        var response = await userServices.GetUserByIdAsync(id);
        return response.Match(
            Ok,
            Problem
        );
    }
    [HttpGet]
    [Route(ApiRoutes.Users.GetUserByEmail)]
    public async Task<IActionResult> GetUserByEmailAsync(string email)
    {
        var response = await userServices.GetUserByEmailAsync(email);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPut]
    [Route(ApiRoutes.Users.UpdateUser)]
    [Authorize]
    public async Task<IActionResult> UpdateUserAsync(UpdateUserRequestDto updateUserDto)
    {
        var response = await userServices.UpdateUserAsync(updateUserDto);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPost]
    [Route(ApiRoutes.Users.ChangePassword)]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync(ChangePasswordRequestDto changePasswordDto)
    {
        var response = await userServices.ChangePasswordAsync(changePasswordDto);
        return response.Match(
            Ok,
            Problem
        );
    }

    [HttpPost]
    [Route(ApiRoutes.Users.UpdateProfileImage)]
    [Authorize]
    public async Task<IActionResult> UpdateProfileImageAsync(IFormFile? file)
    {
        var response = await userServices.UpdateProfileImageAsync(file);
        return response.Match(
                       Ok,
                                  Problem
                                         );
    }
    [HttpDelete]
    [Route(ApiRoutes.Users.DeleteProfileImage)]
    [Authorize]
    public async Task<IActionResult> DeleteProfileImageAsync()
    {
        var response = await userServices.DeleteProfileImageAsync();
        return response.Match(
                                  Ok,
                                  Problem
                             );
    }

    [HttpDelete]
    [Route(ApiRoutes.Users.DeleteAccount)]
    [Authorize]
    public async Task<IActionResult> DeleteAccountAsync()
    {
        var response = await userServices.DeleteAccountAsync();
        return response.Match(
                       Ok,
                                  Problem
                                         );
    }
}
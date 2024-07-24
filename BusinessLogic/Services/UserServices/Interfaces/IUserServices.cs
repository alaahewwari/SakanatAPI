using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.DTOs.UserDtos.Responses;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Sieve.Models;

namespace BusinessLogic.Services.UserServices.Interfaces;

public interface IUserServices
{
    Task<ErrorOr<ProfileImageResponseDto>> UpdateProfileImageAsync(IFormFile? file);
    Task<ErrorOr<SuccessResponse>> DeleteProfileImageAsync();
    Task<ErrorOr<PagedResult<GetUserResponseDto>>> GetAllUsersAsync(SieveModel sieveModel, string? fullName);
    Task<ErrorOr<UserOverviewResponseDto>> GetUserByIdAsync(Guid userId);
    Task<ErrorOr<GetUserResponseDto>> GetUserByEmailAsync(string email);
    Task<ErrorOr<SuccessResponse>> ChangePasswordAsync(ChangePasswordRequestDto request);
    Task<ErrorOr<SuccessResponse>> UpdateUserAsync(UpdateUserRequestDto request);
    Task<ErrorOr<SuccessResponse>> DeleteAccountAsync();
}
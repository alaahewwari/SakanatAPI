using BusinessLogic.DTOs.AuthenticationDtos.Responses;
using DataAccess.Models;
using ErrorOr;

namespace BusinessLogic.Services.AuthenticationServices.Interfaces;

public interface ITokenServices
{
    Task<ErrorOr<TokenResponseDto?>> GetJwtTokenAsync(ApplicationUser user);
    Task<ErrorOr<TokenResponseDto>> ValidateRefreshTokenAsync(ApplicationUser user, string refreshToken);
}
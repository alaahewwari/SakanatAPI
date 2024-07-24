using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.DTOs.AuthenticationDtos.Responses;
using ErrorOr;

namespace BusinessLogic.Services.AuthenticationServices.Interfaces;

public interface ILoginServices
{
    Task<ErrorOr<LoginResponse>> LoginUserWithJwtTokenAsync(UserLoginRequestDto request);
     Task<ErrorOr<LoginResponse>> RefreshTokenAsync(string refreshToken);
}
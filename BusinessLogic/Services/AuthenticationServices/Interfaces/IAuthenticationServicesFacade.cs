using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.DTOs.AuthenticationDtos.Responses;
using ErrorOr;

namespace BusinessLogic.Services.AuthenticationServices.Interfaces;

public interface IAuthenticationServicesFacade
{
    Task<ErrorOr<string>> RegisterUserAsync(UserRegistrationRequestDto request,string url);
    Task<ErrorOr<SuccessResponse>> ConfirmEmailAsync(string email,string token);
    Task<ErrorOr<LoginResponse>> LoginUserAsync(UserLoginRequestDto request);
    Task<ErrorOr<LoginResponse>> RefreshTokenAsync(string token);
    Task<ErrorOr<string>> ForgotPasswordAsync(string email, string url);
    Task<ErrorOr<SuccessResponse>> ResetPasswordAsync(ResetPasswordRequestDto request);
    Task<ErrorOr<SuccessResponse>> VerifyUserTokenAsync(string token, string email);
}
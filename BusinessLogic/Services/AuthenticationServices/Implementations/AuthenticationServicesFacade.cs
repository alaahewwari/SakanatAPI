using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.DTOs.AuthenticationDtos.Responses;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using ErrorOr;
namespace BusinessLogic.Services.AuthenticationServices.Implementations;
public class AuthenticationServicesFacade(
    ILoginServices loginServices,
    IRegistrationServices registrationServices,
    IPasswordServices passwordServices)
    : IAuthenticationServicesFacade
{
    public async Task<ErrorOr<LoginResponse>> LoginUserAsync(UserLoginRequestDto request)
    {
        return await loginServices.LoginUserWithJwtTokenAsync(request);
    }
    public async Task<ErrorOr<LoginResponse>> RefreshTokenAsync(string token)
    {
        return await loginServices.RefreshTokenAsync(token);
    }
    public async Task<ErrorOr<string>> RegisterUserAsync(UserRegistrationRequestDto request,string url)
    {
         return await registrationServices.GenerateAndSendEmailConfirmationToken(request,url);
    }
    public async Task<ErrorOr<SuccessResponse>> ConfirmEmailAsync(string email,string token)
    {
        var decodedToken = Uri.UnescapeDataString(token);
        return await registrationServices.ConfirmEmailAsync(email, decodedToken);
    }
    public async Task<ErrorOr<string>> ForgotPasswordAsync(string email,string url)
    {
        return await passwordServices.GenerateForgotPasswordTokenAsync(email,url);
    }
    public async Task<ErrorOr<SuccessResponse>> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        return await passwordServices.ResetPasswordAsync(request);
    }
    public async Task<ErrorOr<SuccessResponse>> VerifyUserTokenAsync(string token,string email)
    {
        return await passwordServices.VerifyUserTokenAsync(token,email);
    }
    }
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using BusinessLogic.Services.EmailServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using ErrorOr;

namespace BusinessLogic.Services.AuthenticationServices.Implementations;

public class PasswordServices(
    IIdentityManager identityManager,
    IEmailServices emailServices) 
    : IPasswordServices
{
    public async Task<ErrorOr<string>> GenerateForgotPasswordTokenAsync(string email,string url)
    {
        var user = await identityManager.FindByEmailAsync(email);
        if (user == null)
        {
            return Errors.Identity.UserNotFound;
        }
        var token = await identityManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);
        var resetPasswordUrl = $"{url}&token={encodedToken}";
        await emailServices.SendPasswordResetEmailAsync(
            email!,
            resetPasswordUrl);
        return encodedToken;
    }

    public async Task<ErrorOr<SuccessResponse>> ResetPasswordAsync(ResetPasswordRequestDto request)
    {
        var user = await identityManager.FindByEmailAsync(request.Email);
        var result = user != null && await identityManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (result)
        {
            return new SuccessResponse("Password Reset Successfully");
        }
        return Errors.Identity.InvalidToken;
    }
    public async Task<ErrorOr<SuccessResponse>> VerifyUserTokenAsync(string token, string email)
    {
var user = await identityManager.FindByEmailAsync(email);
        var decodedToken = Uri.UnescapeDataString(token);
        var result = user != null && await identityManager.VerifyUserTokenAsync(user, decodedToken);
        if (result)
        {
            return new SuccessResponse("Token Verified Successfully");
        }
        return Errors.Identity.InvalidToken;
    }

}
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using DataAccess.Models;
using ErrorOr;

namespace BusinessLogic.Services.AuthenticationServices.Interfaces;

public interface IPasswordServices
{
    Task<ErrorOr<string>> GenerateForgotPasswordTokenAsync(string email, string url);
    Task<ErrorOr<SuccessResponse>> ResetPasswordAsync(ResetPasswordRequestDto request);
    Task<ErrorOr<SuccessResponse>> VerifyUserTokenAsync(string token, string email);
}
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using ErrorOr;

namespace BusinessLogic.Services.AuthenticationServices.Interfaces;

public interface IRegistrationServices
{
    Task<ErrorOr<string>> GenerateAndSendEmailConfirmationToken(UserRegistrationRequestDto request,string url);
    Task<ErrorOr<SuccessResponse>> ConfirmEmailAsync(string email,string token);
}
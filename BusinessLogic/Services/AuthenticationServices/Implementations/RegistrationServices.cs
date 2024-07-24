using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using BusinessLogic.Services.EmailServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
namespace BusinessLogic.Services.AuthenticationServices.Implementations;
public class RegistrationServices(
    IUnitOfWork unitOfWork,
    IIdentityManager identityManager,
    ICityRepository cityRepository,
    IEmailServices emailServices)
    : IRegistrationServices
{
    public async Task<ErrorOr<string>> GenerateAndSendEmailConfirmationToken(UserRegistrationRequestDto request,string url)
    {
        var user = await identityManager.FindByEmailAsync(request.Email);
        if (user is not null)
        {
            return Errors.Identity.UserAlreadyExist;
        }
        var roleExist = await identityManager.RoleExistsAsync(request.Role);
        if (!roleExist)
        {
            return Errors.User.RoleDoesNotExist;
        }
        var role = await identityManager.GetRoleByNameAsync(request.Role);
        if (role is null)
        {
            return Errors.User.RoleDoesNotExist;
        }
        var city = await cityRepository.GetCityByNameAsync(request.CityName);
        if (city is null)
        {
            return Errors.City.CityNotFound;
        }
        try
        {
            await unitOfWork.BeginTransactionAsync();
            var applicationUser = new ApplicationUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                CityId = city.Id,
                CreationDate = DateOnly.FromDateTime(DateTime.Now),
                PhoneNumber = request.PhoneNumber,
                RoleId = role.Id,
            };
            var createUserResult = await identityManager.CreateAsync(applicationUser, request.Password);
            if (!createUserResult)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Errors.Identity.FailedToCreateUser;
            }
            var token = await identityManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            if (token is null)
            {
                await unitOfWork.RollbackTransactionAsync();
                return Errors.Identity.InvalidToken;
            }
            var encodedToken = Uri.EscapeDataString(token);
            var confirmationUrl = $"{url}&token={encodedToken}";
            await emailServices.SendConfirmationEmailAsync(request.Email!, confirmationUrl);
            // If all operations successful, commit transaction
            await unitOfWork.CommitTransactionAsync();
            return token;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw;
        }

    }
    public async Task<ErrorOr<SuccessResponse>> ConfirmEmailAsync(string email,string token)
    {
        var user = await identityManager.FindByEmailAsync(email);
        if (user is null)
        {
            return Errors.Identity.UserNotFound;
        }
        var result = await identityManager.ConfirmEmailAsync(user, token);
        if (!result)
        {
            return Errors.Unknown.Create("Failed to confirm email");
        }
        return new SuccessResponse("Email Confirmed Successfully");
    } 
}
using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using BusinessLogic.DTOs.AuthenticationDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using ErrorOr;
namespace BusinessLogic.Services.AuthenticationServices.Implementations;
public class LoginServices(
    ITokenServices tokenServices,
    IIdentityManager identityManager,
    IUserRepository userRepository
    )
    : ILoginServices
{
    public async Task<ErrorOr<LoginResponse>> LoginUserWithJwtTokenAsync(UserLoginRequestDto request)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);
        if (user == null)
            return Errors.Identity.UserNotFound;

        if (await identityManager.IsEmailConfirmedAsync(user))
        {
            if (await identityManager.CheckPasswordAsync(user, request.Password))
            {
                var token = await tokenServices.GetJwtTokenAsync(user);
               
                if (token.Value is not null)
                {
                    var loginResponse = new LoginResponse
                    {
                        AccessToken = token.Value.AccessToken.Token,
                        RefreshToken = token.Value.RefreshToken.Token,
                        AccessTokenExpiryDate = token.Value.AccessToken.ExpiryTokenDate,
                        RefreshTokenExpiryDate = token.Value.RefreshToken.ExpiryTokenDate
                    };
                    return loginResponse;
                }

                return Errors.Identity.InvalidToken;
            }

            return Errors.Identity.InvalidCredentials;
        }

        return Errors.Identity.EmailNotConfirmed;
    }
    public async Task<ErrorOr<LoginResponse>> RefreshTokenAsync(string refreshToken)
    {
        var user = await userRepository.FindByRefreshTokenAsync(refreshToken);
        if (user == null)
            return Errors.Identity.InvalidToken;


        var token = await tokenServices.ValidateRefreshTokenAsync(user,refreshToken);
        if (token.IsError)
            return Errors.Identity.InvalidToken;
        return new LoginResponse
        {
            AccessToken = token.Value.AccessToken.Token,
            RefreshToken = token.Value.RefreshToken.Token,
            AccessTokenExpiryDate = token.Value.AccessToken.ExpiryTokenDate,
            RefreshTokenExpiryDate = token.Value.RefreshToken.ExpiryTokenDate
        };
    }
}
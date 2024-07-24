using BusinessLogic.DTOs.AuthenticationDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Infrastructure.Authentication;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.AuthenticationServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
namespace BusinessLogic.Services.AuthenticationServices.Implementations;
public class TokenServices(
    UserManager<ApplicationUser> userManager,
    IUserRepository userRepository,
    IConfiguration configuration)
    : ITokenServices
{
    public async Task<ErrorOr<TokenResponseDto>> GetJwtTokenAsync(ApplicationUser user)
    {
        var refreshToken = GenerateRefreshToken();
        _ = int.TryParse(configuration["JWT:RefreshTokenValidity"], out var refreshTokenValidity);
        var role = await userRepository.GetUserRoleAsync(user);
        var authClaims = new ClaimsIdentity(new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim("userId", user.Id.ToString().ToUpper()),
            new Claim("RoleName", role.Name!),
            new Claim("refreshToken", refreshToken),
            new Claim (ClaimTypes.Role, role.Name!)
        });
        var jwtToken = GetToken(authClaims);
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenValidity)
        });
        await userManager.UpdateAsync(user);

        return new TokenResponseDto
        {
            AccessToken = new JwtToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiryTokenDate = jwtToken.ValidTo
            },
            RefreshToken = new JwtToken
            {
                Token = refreshToken,
                ExpiryTokenDate = DateTime.UtcNow.AddDays(refreshTokenValidity)
            }
        };
    }
    public async Task<ErrorOr<TokenResponseDto>> ValidateRefreshTokenAsync(ApplicationUser user, string refreshToken)
    {
        var existRefreshToken = user.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);
        if (existRefreshToken is null)
            return Errors.Identity.InvalidToken;
        if (existRefreshToken.ExpiresAt < DateTime.UtcNow)
        {
            user.RefreshTokens.Remove(existRefreshToken);
            await userManager.UpdateAsync(user);
        }
        var token = await GetJwtTokenAsync(user);
        var userTokens = user.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);
        if (userTokens is null)
        {
            return token;
        }
        existRefreshToken.Token = token.Value.RefreshToken.Token;
        existRefreshToken.ExpiresAt = token.Value.RefreshToken.ExpiryTokenDate;
        await userManager.UpdateAsync(user);
        return token.Value;
    }
    private JwtSecurityToken GetToken(ClaimsIdentity authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? string.Empty));

        _ = int.TryParse(configuration["JWT:TokenValidityInMinutes"], out var tokenValidityInMinutes);
        var expirationTimeUtc = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);
        var localTimeZone = TimeZoneInfo.Local;
        var expirationTimeInLocalTimeZone = TimeZoneInfo.ConvertTimeFromUtc(expirationTimeUtc, localTimeZone);

        var token = new JwtSecurityToken(
            configuration["JWT:ValidIssuer"],
            configuration["JWT:ValidAudience"],
            expires: expirationTimeInLocalTimeZone,
            claims: authClaims.Claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        var range = RandomNumberGenerator.Create();
        range.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
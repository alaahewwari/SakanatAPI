using BusinessLogic.DTOs.UserDtos.Responses;
using DataAccess.Models;
using System.Security.Claims;
namespace BusinessLogic.Services.IdentityServices.Interfaces;
public interface IIdentityManager
{
    Task<UserOverviewResponseDto?> GetUserByIdAsync(Guid userId);
    Task<GetUserResponseDto> GetUserByEmailAsync(string email);
    Task<ApplicationUser?> FindByIdAsync(Guid userId);
    Task<ApplicationUser?> FindByEmailAsync(string email);
    Task<ApplicationUser?> GetLoggedInUserIdAsync();
    Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal user);
    Task<bool> CreateAsync(ApplicationUser newUser, string password);
    Task<bool> AddToRoleAsync(ApplicationUser user, string role);
    Task<string?> GenerateEmailConfirmationTokenAsync(ApplicationUser user);
    Task<bool> IsEmailConfirmedAsync(ApplicationUser user);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<Role> GetRoleAsync(ApplicationUser user);
    Task<Role?> GetRoleByNameAsync(string role);
    Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
    Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword);
    Task<bool> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword);
    Task<bool> RoleExistsAsync(string role);
    Task UpdateAsync(ApplicationUser user);
    Task<bool> ConfirmEmailAsync(ApplicationUser user, string token);
    Task<bool> VerifyUserTokenAsync(ApplicationUser user, string token);

}
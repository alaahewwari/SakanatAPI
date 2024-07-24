using AutoMapper.QueryableExtensions;
using AutoMapper;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.Services.IdentityServices.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;
using System.Security.Claims;
using BusinessLogic.Repositories.Interfaces;
namespace BusinessLogic.Services.IdentityServices.Implementations;
public class IdentityManager(
    UserManager<ApplicationUser> userManager,
    RoleManager<Role> roleManager,
    ISieveProcessor sieveProcessor,
    IUnitOfWork unitOfWork,
    IHttpContextAccessor httpContext,
    IMapper mapper)
    : IIdentityManager
{

    public async Task<ApplicationUser?> GetUserAsync(ClaimsPrincipal user)
    {
        return await userManager.GetUserAsync(user);
    }
    public async Task<bool> AddToRoleAsync(ApplicationUser user, string role)
    {
        var result = await userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }
    public async Task<ApplicationUser?> GetLoggedInUserIdAsync()
    {
        var userId = httpContext.HttpContext!.User.FindFirstValue("userId");
        if (userId is null)
        {
            return null;
        }
        var user = await FindByIdAsync(Guid.Parse(userId!));
        return user;
    }
    public async Task<bool> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
    {
        var result = await userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }
    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return await userManager.CheckPasswordAsync(user, password);
    }
    public async Task<bool> CreateAsync(ApplicationUser newUser, string password)
    {
        var result = await userManager.CreateAsync(newUser, password);
        return result.Succeeded;
    }
    public async Task<ApplicationUser?> FindByEmailAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user;
    }
    public async Task<GetUserResponseDto> GetUserByEmailAsync(string email)
    {
        var user=await userManager.Users.Where(u => u.Email == email)
            .ProjectTo<GetUserResponseDto>(mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
        return user;
    }
    public async Task<ApplicationUser?> FindByIdAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        return user;
    }
    public async Task<UserOverviewResponseDto?> GetUserByIdAsync(Guid userId)
    {
        var user =await userManager.Users
       .Where(u => u.Id == userId)
       .ProjectTo<UserOverviewResponseDto>(mapper.ConfigurationProvider).FirstOrDefaultAsync();
        return user;
    }
    public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
    {
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }
    public async Task<Role> GetRoleAsync(ApplicationUser user)
    {
        var roleId = user.RoleId;
        var role = await roleManager.FindByIdAsync(roleId.ToString());
        return role;
    }
    public async Task<Role?> GetRoleByNameAsync(string role)
    {
        return await roleManager.FindByNameAsync(role);
    }
    public Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
    {
        return userManager.IsEmailConfirmedAsync(user);
    }
    public async Task<bool> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
    {
        var result = await userManager.ResetPasswordAsync(user, token, newPassword);
        return result.Succeeded;
    }
    public async Task<bool> RoleExistsAsync(string role)
    {
        return await roleManager.RoleExistsAsync(role);
    }
    public async Task<string?> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
    {
        return await userManager.GenerateEmailConfirmationTokenAsync(user);
    }
    public async Task UpdateAsync(ApplicationUser user)
    {
        await userManager.UpdateAsync(user);
    }
    public async Task<bool> ConfirmEmailAsync(ApplicationUser user, string token)
    {
        var result = await userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            await UpdateAsync(user);
            await unitOfWork.SaveChangesAsync();
            return true;
        }
        return false;
    }
public async Task<bool> VerifyUserTokenAsync(ApplicationUser user, string token)
    {
        var tokenProvider = userManager.Options.Tokens.PasswordResetTokenProvider;
        var result = await userManager.VerifyUserTokenAsync(user, tokenProvider, "ResetPassword", token);
        return result;
    }
}
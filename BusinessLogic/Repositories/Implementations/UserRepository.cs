using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
namespace BusinessLogic.Repositories.Implementations
{
    public class UserRepository(
        ApplicationDbContext context,
        ISieveProcessor sieveProcessor,
        IMapper mapper)
        : IUserRepository
    {
        public async Task<PagedResult<GetUserResponseDto>> GetAllUsersAsync(SieveModel sieveModel, string? fullname)
        {
            sieveModel.SetDefaultPagination();
            IQueryable<ApplicationUser> usersQuery = context.Users.AsNoTracking().Where(u =>u.Email!=null);

            if (!string.IsNullOrEmpty(fullname))
            {
                fullname = fullname.Trim();
                var names = fullname.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (names.Length > 1)
                {
                    var firstName = "%" + names[0] + "%";
                    var lastName = "%" + names[1] + "%";
                    usersQuery = context.Users.FromSqlInterpolated(
                        $@"SELECT * FROM Users WHERE FirstName LIKE {firstName} AND LastName LIKE {lastName} AND Email IS NOT NULL");
                }
                else
                {
                    var nameLike = "%" + fullname + "%";
                    usersQuery = context.Users.FromSqlInterpolated(
                        $@"SELECT * FROM Users WHERE (FirstName LIKE {nameLike} OR LastName LIKE {nameLike}) AND Email IS NOT NULL");
                }
            }
            var filteredUsers = sieveProcessor.Apply(sieveModel, usersQuery, applyPagination: false);
            var totalCount = await filteredUsers.CountAsync();
            var paginatedUsers = sieveProcessor.Apply(sieveModel, usersQuery);
            var projectedUsers = await paginatedUsers
                .ProjectTo<GetUserResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return new PagedResult<GetUserResponseDto>(
                projectedUsers,
                totalCount,
                sieveModel.Page!.Value,
                sieveModel.PageSize!.Value
            );
        }
        public async Task<ApplicationUser?> FindByRefreshTokenAsync(string refreshToken)
        {
            var user=await context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
            return user;
        }
        public async Task DeleteUserAsync(ApplicationUser user)
        {
            var result = context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
        public async Task<ApplicationUser?> FindByEmailAsync(string email)
        {
            var user =await context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
        public async Task<bool> DeleteProfileImageAsync (ApplicationUser user)
        {
            user.ImagePath = null;
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        public async Task<Role> GetUserRoleAsync(ApplicationUser user)
        {
            var userRole = await context.Roles.FirstOrDefaultAsync(r => r.Id == user.RoleId);
            return userRole;
        }
    }
}

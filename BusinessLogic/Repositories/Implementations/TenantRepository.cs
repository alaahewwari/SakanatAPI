using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.TenantsDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace BusinessLogic.Repositories.Implementations
{
    public class TenantRepository(
        ISieveProcessor sieveProcessor,
        IMapper mapper,
        ApplicationDbContext context
        ) : ITenantRepository
    {
        public async Task<TenantResponseDto?> GetTenantByIdAsync(Guid id)
        {
            var tenant = await context.Tenants
                .AsNoTracking()
                .ProjectTo<TenantResponseDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);
            return tenant;
        }
        public async Task<PagedResult<TenantResponseDto>> GetTenantsByOwnerIdAsync(Guid ownerId, SieveModel sieveModel, string? fullName)
        {
            sieveModel.SetDefaultPagination();
            //IQueryable<Tenant> usersQuery = context.Tenants.AsNoTracking().Where(x => x.OwnerId == ownerId);
            IQueryable<Tenant> tenantQuery = context.Users
                .AsNoTracking()
                .Where(u => u.Id == ownerId)
                .SelectMany(u => u.Tenants);

            if (!string.IsNullOrEmpty(fullName))
            {
                fullName = fullName.Trim();
                var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (names.Length > 1)
                {
                    // Adds '%' wildcards before and after each name to allow matching any part of the name
                    var firstName = "%" + names[0] + "%";
                    var lastName = "%" + names[1] + "%";
                    tenantQuery = context.Tenants.FromSqlInterpolated(
                        $@"SELECT * FROM Users WHERE FirstName LIKE {firstName} AND LastName LIKE {lastName} AND Email IS NULL");
                }
                else
                {
                    // Adds a '%' wildcard before and after the name to allow matching any part of either the first name or last name
                    var nameLike = "%" + fullName + "%";
                    tenantQuery = context.Tenants.FromSqlInterpolated(
                        $@"SELECT * FROM Users WHERE (FirstName LIKE {nameLike} OR LastName LIKE {nameLike} AND Email IS NULL)");
                }
            }
            var filteredUsers = sieveProcessor.Apply(sieveModel, tenantQuery, applyPagination: false);
            var totalCount = await filteredUsers.CountAsync();
            var paginatedUsers = sieveProcessor.Apply(sieveModel, tenantQuery);
            var projectedUsers = await paginatedUsers
                .ProjectTo<TenantResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();
            return new PagedResult<TenantResponseDto>(
                projectedUsers,
                totalCount,
                sieveModel.Page!.Value,
                sieveModel.PageSize!.Value
            );
        }
        public async Task<decimal> GetTerminatedContractsPercentageAsync(Guid ownerId)
        {
            // Fetch all tenants for the given owner
            var allTenants = await context.Tenants
                .Where(tenant => tenant.OwnerId == ownerId
                                 && tenant.Contracts.Any())
                .Include(tenant => tenant.Contracts)
                .ToListAsync();

            // Count tenants with at least one non-terminated contract
            var tenantsWithActiveContracts = allTenants.Count(tenant =>
                tenant.Contracts.Any(contract => contract.IsTerminated == false));

            // Total number of tenants
            var totalTenants = allTenants.Count;

            // Calculate the percentage
            decimal percentageWithActiveContracts = totalTenants == 0 ? 0 :
                (decimal)tenantsWithActiveContracts / totalTenants * 100;

            return percentageWithActiveContracts;

        }
        public async Task<Tenant?> CreateTenantAsync(Tenant tenant, Guid ownerId)
        {
            await context.Tenants.AddAsync(tenant);
            await context.SaveChangesAsync();
            return tenant;
        }
        public async Task<bool> UpdateTenantAsync(Guid id, Tenant tenant)
        {
            var tenantToUpdate = await context.Tenants.FirstOrDefaultAsync(c => c.Id == id);
            if (tenantToUpdate is not null)
            {
                tenantToUpdate.PhoneNumber = tenant.PhoneNumber;
                tenantToUpdate.FirstName = tenant.FirstName;
                tenantToUpdate.LastName = tenant.LastName;
                tenantToUpdate.CityId = tenant.CityId;
                tenantToUpdate.Note = tenant.Note;
                await context.SaveChangesAsync();
                return true;
            }
            return false;

        }
        public async Task<bool> DeleteTenantAsync(Tenant tenant)
        {
            context.Tenants.Remove(tenant);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteAllTenantsAsync()
        {
            var tenants = await context.Tenants.ToListAsync();
            context.Tenants.RemoveRange(tenants);
            await context.SaveChangesAsync();
            return true;
        }
        public async Task<Contract?> CreateContractAsync(Contract contract)
        {
 await context.Contracts.AddAsync(contract);
            await context.SaveChangesAsync();
            return contract;
        }
        public async Task<bool> GetTenantContractByTimeAsync(Guid tenantId, DateOnly startDate, DateOnly endDate)
        {
            var tenant = await context.Tenants
                .Include(x => x.Contracts)
                .FirstOrDefaultAsync(x => x.Id == tenantId);
            if (tenant is null)
            {
                return false;
            }
            var contract = tenant.Contracts.FirstOrDefault(x => x.StartDate <= startDate && x.EndDate >= endDate);
            return contract is not null;
        }
        public async Task<Tenant?> FindByEmailAsync(string email)
        {
            var tenant = await context.Tenants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
            return tenant;
        }
    }
}

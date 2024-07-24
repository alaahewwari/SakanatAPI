using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.TenantsDtos.Responses;
using DataAccess.Models;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface ITenantRepository
    {
        Task<TenantResponseDto?> GetTenantByIdAsync(Guid id);
        Task<PagedResult<TenantResponseDto>> GetTenantsByOwnerIdAsync(Guid ownerId,SieveModel sieveModel, string? fullName);
        Task<decimal> GetTerminatedContractsPercentageAsync(Guid ownerId);
        Task<Tenant?> CreateTenantAsync(Tenant tenant, Guid ownerId);
        //FindByEmailAsync
        Task<Tenant?> FindByEmailAsync(string email);
        Task<bool> UpdateTenantAsync(Guid id, Tenant tenant);
        Task<bool> DeleteTenantAsync(Tenant tenant);
        Task<bool> DeleteAllTenantsAsync();
        Task<Contract?> CreateContractAsync(Contract contract);
        Task<bool> GetTenantContractByTimeAsync(Guid tenantId,DateOnly startDate,DateOnly endDate);
    }
}

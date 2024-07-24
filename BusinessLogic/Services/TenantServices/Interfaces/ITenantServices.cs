
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.TenantsDtos.Requests;
using BusinessLogic.DTOs.TenantsDtos.Responses;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.TenantServices.Interfaces
{
    public interface ITenantServices
    {
        Task<ErrorOr<TenantResponseDto>> GetTenantByIdAsync(Guid id);
        Task<ErrorOr<decimal>> GetTerminatedContractsPercentageAsync(Guid ownerId);
        Task<ErrorOr<PagedResult<TenantResponseDto>>> GetTenantByOwnerIdAsync(Guid ownerId, SieveModel sieveModel, string? fullName);
        Task<ErrorOr<TenantResponseDto>> CreateTenantAsync(TenantRequestDto request);
        Task<ErrorOr<SuccessResponse>> UpdateTenantAsync(Guid id, TenantRequestDto request);
        Task<ErrorOr<SuccessResponse>> DeleteTenantAsync(Guid id);
        Task<ErrorOr<SuccessResponse>> DeleteAllTenantsAsync();
        Task<ErrorOr<SuccessResponse>> CreateContractAsync(Guid tenantId, Guid apartmentId,
            CreateContractRequestDto contract);

    }
}

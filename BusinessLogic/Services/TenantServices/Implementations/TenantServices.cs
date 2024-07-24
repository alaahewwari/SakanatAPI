using AutoMapper;
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.TenantsDtos.Requests;
using BusinessLogic.DTOs.TenantsDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.TenantServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;
namespace BusinessLogic.Services.TenantServices.Implementations
{
    public class TenantServices(
        ITenantRepository tenantRepository,
        IIdentityManager identityManager,
        ICityRepository cityRepository,
        IApartmentRepository apartmentRepository,
        IMapper mapper
        ): ITenantServices
    {
public async Task<ErrorOr<decimal>> GetTerminatedContractsPercentageAsync(Guid ownerId)
        {
var owner = await identityManager.GetUserByIdAsync(ownerId);
            if (owner is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var response = await tenantRepository.GetTerminatedContractsPercentageAsync(ownerId);
            return response;
        }
        public async Task<ErrorOr<TenantResponseDto>> GetTenantByIdAsync(Guid id)
        {
            var response = await tenantRepository.GetTenantByIdAsync(id);
            if (response is null)
            {
                return Errors.Tenant.TenantNotFound;
            }
            return response;
        }

        public async Task<ErrorOr<PagedResult<TenantResponseDto>>> GetTenantByOwnerIdAsync(Guid ownerId, SieveModel sieveModel, string? fullName)
        {
            var owner = await identityManager.GetUserByIdAsync(ownerId);
            if (owner is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var response = await tenantRepository.GetTenantsByOwnerIdAsync(ownerId, sieveModel,fullName);
            return response;
        }

        public async Task<ErrorOr<TenantResponseDto>> CreateTenantAsync(TenantRequestDto request)
        {
            var owner = await identityManager.GetLoggedInUserIdAsync();
            if (owner is null)
            {
                return Errors.Identity.Unauthorized;
            }
            var city = await cityRepository.GetCityByIdAsync(request.CityId);
            if (city is null)
            {
                return Errors.City.CityNotFound;
            }
            //take customer role 
            var role = await identityManager.GetRoleByNameAsync("Customer");
            if (role is null)
            {
                return Errors.User.RoleDoesNotExist;
            }
            var tenant = mapper.Map<Tenant>(request);
            tenant.OwnerId = owner.Id;
            tenant.CityId = city.Id;
            tenant.CreationDate = DateOnly.FromDateTime(DateTime.Now);
            tenant.RoleId = role.Id;
            var result = await tenantRepository.CreateTenantAsync(tenant,owner.Id);
            if (result is null)
            {
                return Errors.Unknown.Create("Failed to create tenant");
            }
            var response = mapper.Map<TenantResponseDto>(result);
            return response;
        }
        public async Task<ErrorOr<SuccessResponse>> UpdateTenantAsync(Guid id, TenantRequestDto request)
        {
            var tenant = await tenantRepository.GetTenantByIdAsync(id);
            if (tenant is null)
            {
                return Errors.Tenant.TenantNotFound;
            }
            var city = await cityRepository.GetCityByIdAsync(request.CityId);
            if (city is null)
            {
                return Errors.City.CityNotFound;
            }
            var updatedTenant = mapper.Map<Tenant>(request);
            var response = await tenantRepository.UpdateTenantAsync(id, updatedTenant);
            if (!response)
            {
                return Errors.Unknown.Create("Failed to update tenant");
            }
            return new SuccessResponse("Tenant updated successfully");
        }

        public async Task<ErrorOr<SuccessResponse>> DeleteTenantAsync(Guid id)
        {
            var existTenant = await tenantRepository.GetTenantByIdAsync(id);
            if (existTenant is null)
            {
                return Errors.Tenant.TenantNotFound;
            }
            var tenant = mapper.Map<Tenant>(existTenant);
            var response = await tenantRepository.DeleteTenantAsync(tenant);
            if (!response)
            {
                return Errors.Unknown.Create("Failed to delete tenant");
            }
            return new SuccessResponse("Tenant deleted successfully");
        }

        public async Task<ErrorOr<SuccessResponse>> DeleteAllTenantsAsync()
        {
            var response = await tenantRepository.DeleteAllTenantsAsync();
            if (!response)
            {
                return Errors.Unknown.Create("Failed to delete tenants");
            }
            return new SuccessResponse("Tenants deleted successfully");
        }

        public async Task<ErrorOr<SuccessResponse>> CreateContractAsync(Guid tenantId, Guid apartmentId, CreateContractRequestDto contract)
        {
            var tenant = await tenantRepository.GetTenantByIdAsync(tenantId);

            if (tenant is null)
            {
                return Errors.Tenant.TenantNotFound;
            }

            var tenantHasContractOnThisTime = await tenantRepository.GetTenantContractByTimeAsync(tenantId, contract.StartDate, contract.EndDate);
            if (tenantHasContractOnThisTime)
            {
                return Errors.Tenant.TenantHasContractOnThisTime;
            }

            var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);

            if (apartment is null)
            {
                return Errors.Apartment.ApartmentNotFound;
            }

            var newContract = mapper.Map<Contract>(contract);   
            newContract.TenantId = tenantId;
            newContract.ApartmentId = apartmentId;
            newContract.IsTerminated = false;

            var result = await tenantRepository.CreateContractAsync(newContract);
            if (result is null)
            {
                return Errors.Unknown.Create("Failed to create contract");
            }
            
            return new SuccessResponse("Contract created successfully");
        }

    }
}

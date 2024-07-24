using System.Text.Json;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.TenantsDtos.Requests;
using BusinessLogic.Services.TenantServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers
{
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class TenantsController(ITenantServices tenantServices) : ApiController
    {
        [HttpGet]
        [Route(ApiRoutes.Tenants.GetAllTenantsByOwnerId)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetTenantByOwnerId([FromRoute] Guid ownerId, [FromQuery] SieveModel sieveModel, string? fullName)
        {
            var response = await tenantServices.GetTenantByOwnerIdAsync(ownerId, sieveModel, fullName);
            return response.Match(
                data =>
                {
                    var result = data;
                    var items = result.Items;
                    Response.Headers["X-Pagination"] = JsonSerializer.Serialize(result.PaginationMetadata);
                    return Ok(items);
                },
                Problem
            ); ;
        }

        [HttpGet]
        [Route(ApiRoutes.Tenants.NumberOfTenants)]
        [Authorize]
        public async Task<IActionResult> GetTerminatedContractsPercentage([FromRoute] Guid ownerId)
        {
            var response = await tenantServices.GetTerminatedContractsPercentageAsync(ownerId);
            return Ok(response.Value);
        }
        [HttpPost]
        [Route(ApiRoutes.Tenants.CreateTenant)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CreateTenant([FromBody] TenantRequestDto request)
        {
            var response = await tenantServices.CreateTenantAsync(request);
            return response.Match(
                value => CreatedAtAction(nameof(GetTenantById), new { id = value.Id } , value),
                                                                                                                                                                                                                                  Problem
                                                                                                                                                                                                                              );
        }

        [HttpGet]
        [Route(ApiRoutes.Tenants.GetTenantById)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetTenantById([FromRoute] Guid id)
        {
            var response = await tenantServices.GetTenantByIdAsync(id);
            return response.Match(
                                              Ok,
                                                                                           Problem
                                                                                       );
        }
        [HttpPut]
        [Route(ApiRoutes.Tenants.UpdateTenant)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateTenant([FromRoute] Guid id, [FromBody] TenantRequestDto request)
        {
            var response = await tenantServices.UpdateTenantAsync(id, request);
            return response.Match(
                                                                                                            Ok,
                                                                                                                                                                                                                                                                                                                                                                  Problem
                                                                                                                                                                                                                                                                                                                                                              );
        }
        [HttpPost]
        [Route(ApiRoutes.Tenants.CreateContract)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> CreateContractAsync([FromBody] CreateContractRequestDto request, [FromRoute] Guid apartmentId, [FromRoute] Guid tenantId)
        {
            var response = await tenantServices.CreateContractAsync(tenantId, apartmentId, request);
            return response.Match(
                Ok,
                Problem
            );
        }
    }
}

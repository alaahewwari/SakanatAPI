using System.Text.Json;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.PaymentDtos.Requests;
using BusinessLogic.Services.ContractServices.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace Presentation.Controllers
{
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class ContractsController(IContractServices contractServices) : ApiController
    {
        [HttpGet]
        [Route(ApiRoutes.Contract.GetContractByOwnerId)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetContractsByOwnerId([FromRoute] Guid ownerId, [FromQuery] SieveModel sieveModel)
        {
            var response = await contractServices.GetContractByOwnerIdAsync(ownerId, sieveModel);
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
        [Route(ApiRoutes.Contract.GetContractsByTenantId)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetContractsByTenantId([FromRoute] Guid tenantId)
        {
            var contracts = await contractServices.GetContractsByTenantIdAsync(tenantId);
            return contracts.Match(
                               Ok,
                                              Problem
                                                         );
        }
        [HttpGet]
        [Route(ApiRoutes.Contract.GetContractById)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetContractById([FromRoute] Guid id)
        {
            var contract = await contractServices.GetContractByIdAsync(id);
            return contract.Match(                              
                Ok,
                                              Problem
                                                         );
        }
        [HttpGet]
        [Route(ApiRoutes.Contract.GetContractsByApartmentId)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetContractByApartmentId([FromRoute] Guid apartmentId)
        {
            var contract = await contractServices.GetContractsByApartmentIdAsync(apartmentId);
            return contract.Match(
                                              Ok,
                                                                                           Problem
                                                                                                                                                   );
        }

        [HttpGet]
        [Route(ApiRoutes.Contract.GetTotalPaymentsSumByContractId)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetTotalPaymentsSumByContractId([FromRoute] Guid id)
        {
            var totalPaymentsSum = await contractServices.GetTotalPaymentsSumByContractId(id);
            return Ok(totalPaymentsSum);
        }

        [HttpPut]
        [Route(ApiRoutes.Contract.UpdateContract)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdateContract([FromRoute] Guid id, [FromBody] CreateContractRequestDto contract)
        {
            var updatedContract = await contractServices.UpdateContractAsync(id, contract);
            return updatedContract.Match(
                               Ok,
                                              Problem
                                                         );
        }

        [HttpPost]
        [Route(ApiRoutes.Contract.AddPayment)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> AddPayment([FromRoute] Guid id, [FromBody] PaymentRequestDto payment)
        {
            var response = await contractServices.AddPaymentAsync(id, payment);
            return response.Match(
                               Ok,
                                              Problem
                                                         );
        }

        [HttpGet]
        [Route(ApiRoutes.Contract.GetPaymentById)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetPaymentById([FromRoute] Guid paymentId)
        {
            var response = await contractServices.GetPaymentByIdAsync(paymentId);
            return response.Match(
                                              Ok,
                                                                                           Problem
                                                                                                                                                   );

        }

        [HttpGet]
        [Route(ApiRoutes.Contract.GetPaymentsByContractId)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> GetPaymentsByContractId([FromRoute] Guid id)
        {
            var response = await contractServices.GetPaymentsByContractIdAsync(id);
            return response.Match(
                                                             Ok,
                                                                                                                                                       Problem
                                                                                                                                                                                                                                                                                                         );
        }

        [HttpPut]
        [Route(ApiRoutes.Contract.UpdatePayment)]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> UpdatePayment([FromRoute] Guid paymentId, [FromBody] PaymentRequestDto payment)
        {
            var response = await contractServices.UpdatePaymentAsync(paymentId, payment);
            return response.Match(
                                                                            Ok,
                                                                                                                                                                                                                                  Problem
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          );
        }
    }
}

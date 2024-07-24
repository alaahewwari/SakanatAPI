
using AutoMapper;
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.ContractDtos.Responses;
using BusinessLogic.DTOs.PaymentDtos.Requests;
using BusinessLogic.DTOs.PaymentDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.ContractServices.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.ContractServices.Implementations
{
    public class ContractServices(
        IContractRepository contractRepository,
        IApartmentRepository apartmentRepository,
            ITenantRepository tenantRepository,
            IIdentityManager identityManager,
            IMapper mapper)
        : IContractServices
    {
        public async Task<ErrorOr<PagedResult<ContractResponseDto>>> GetContractByOwnerIdAsync(Guid ownerId, SieveModel sieveModel)
        {
            var owner = await identityManager.GetUserByIdAsync(ownerId);
            if (owner is null)
            {
                return Errors.Identity.UserNotFound;
            }
            var contracts = await contractRepository.GetContractsByOwnerIdAsync(ownerId, sieveModel);
            foreach (var contract in contracts.Items)
            {
                contract.TotalPayments = await GetTotalPaymentsSumByContractId(contract.Id);
            }
            return contracts;
        }
        public async Task<ErrorOr<ContractResponseDto>> GetContractByIdAsync(Guid contractId)
        {
            var contract = await contractRepository.GetContractByIdAsync(contractId);
            if (contract is null)
            {
                return Errors.Contracts.ContractNotFound;
            }
            return contract;
        }
        public async Task<ErrorOr<IList<ContractResponseDto>>> GetContractsByTenantIdAsync(Guid tenantId)
        {
            var tenant = await tenantRepository.GetTenantByIdAsync(tenantId);
            if (tenant is null)
            {
                return Errors.Tenant.TenantNotFound;
            }
            var contracts = await contractRepository.GetContractsByTenantIdAsync(tenantId);
            return contracts.ToList();
        }
        public async Task<ErrorOr<IList<ContractResponseDto>>> GetContractsByApartmentIdAsync(Guid apartmentId)
        {
            var apartment = await apartmentRepository.GetApartmentByIdAsync(apartmentId);
            if (apartment is null)
            { 
                return Errors.Apartment.ApartmentNotFound;
            }
            var contracts = await contractRepository.GetContractsByApartmentIdAsync(apartmentId);
            return contracts.ToList();
        }
        public async Task<ErrorOr<SuccessResponse>> UpdateContractAsync(Guid contractId, CreateContractRequestDto contract)
        {
            var existingContract = await contractRepository.GetContractByIdAsync(contractId);
            if (existingContract is null)
            {
                return Errors.Contracts.ContractNotFound;
            }
            var result =mapper.Map<Contract>(contract);
            var updatedContract = await contractRepository.UpdateContractAsync(contractId, result);
            if (updatedContract)
            {
                return new SuccessResponse("Contract updated successfully");
            }
            return Errors.Unknown.Create ("Failed to update contract");
        }
    public async Task<ErrorOr<SuccessResponse>> AddPaymentAsync(Guid contractId, PaymentRequestDto paymentDto)
        {
        var existingContract = await contractRepository.GetContractByIdAsync(contractId);
        if (existingContract is null)
        {
            return Errors.Contracts.ContractNotFound;
        }
        var payment =new PaymentLog
        {
            ContractId = contractId,
            Amount = paymentDto.Amount,
            Date = DateOnly.FromDateTime(DateTime.UtcNow),
        };

            var totalPaymentsSum = await GetTotalPaymentsSumByContractId(contractId);
            if (totalPaymentsSum + payment.Amount > existingContract.RentPrice)
            {
                return Errors.Payment.PaymentAmountExceedsRentPrice;
            }
        var paymentAdded = await contractRepository.AddPaymentRequestAsync(contractId, payment);
        if (paymentAdded)
        {
            return new SuccessResponse("Payment added successfully");
        }
        return Errors.Unknown.Create("Failed to add payment");
        }
public async Task<ErrorOr<PaymentResponseDto>> GetPaymentByIdAsync(Guid paymentId)
        {
        var payment = await contractRepository.GetPaymentByIdAsync(paymentId);
        if (payment is null)
        {
            return Errors.Payment.PaymentNotFound;
        }
        var response = mapper.Map<PaymentResponseDto>(payment);
            return response;
        }
        public async Task<ErrorOr<IList<PaymentResponseDto>>> GetPaymentsByContractIdAsync(Guid contractId)
        {
            var contract = await contractRepository.GetContractByIdAsync(contractId);
            if (contract is null)
            {
                return Errors.Contracts.ContractNotFound;
            }
            var payments = await contractRepository.GetPaymentsByContractIdAsync(contractId);
            var response = mapper.Map<IList<PaymentResponseDto>>(payments);
            return response.ToList();
        }
public async Task<ErrorOr<SuccessResponse>> UpdatePaymentAsync(Guid paymentId, PaymentRequestDto paymentDto)
        {
        var existingPayment = await contractRepository.GetPaymentByIdAsync(paymentId);
        if (existingPayment is null)
        {
            return Errors.Payment.PaymentNotFound;
        }
        var payment = mapper.Map<PaymentLog>(paymentDto);
        var updatedPayment = await contractRepository.UpdatePaymentAsync(paymentId, payment);
        if (updatedPayment)
        {
            return new SuccessResponse("Payment updated successfully");
        }
        return Errors.Unknown.Create("Failed to update payment");
        }
        public async Task<decimal> GetTotalPaymentsSumByContractId(Guid contractId)
        {
            var paymentsSum = await contractRepository.GetTotalPaymentsSumByContractId(contractId);
            return paymentsSum;
        }
    }
}

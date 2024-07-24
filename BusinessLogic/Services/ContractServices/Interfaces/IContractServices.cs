
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.ContractDtos.Responses;
using BusinessLogic.DTOs.PaymentDtos.Requests;
using BusinessLogic.DTOs.PaymentDtos.Responses;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.ContractServices.Interfaces
{
    public interface IContractServices
    {
        Task<ErrorOr<PagedResult<ContractResponseDto>>> GetContractByOwnerIdAsync(Guid ownerId, SieveModel sieveModel);
        Task<ErrorOr<ContractResponseDto>> GetContractByIdAsync (Guid contractId);
        Task<ErrorOr<IList<ContractResponseDto>>> GetContractsByTenantIdAsync(Guid tenantId);
        Task<ErrorOr<IList<ContractResponseDto>>> GetContractsByApartmentIdAsync(Guid apartmentId);
        Task<ErrorOr<SuccessResponse>> UpdateContractAsync(Guid contractId, CreateContractRequestDto contract);
        Task<ErrorOr<SuccessResponse>> AddPaymentAsync(Guid contractId, PaymentRequestDto payment);
        Task<ErrorOr<PaymentResponseDto>> GetPaymentByIdAsync (Guid paymentId);
        Task<ErrorOr<IList<PaymentResponseDto>>> GetPaymentsByContractIdAsync(Guid contractId);
        Task<ErrorOr<SuccessResponse>> UpdatePaymentAsync (Guid paymentId, PaymentRequestDto payment);
        Task<decimal> GetTotalPaymentsSumByContractId(Guid contractId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.ContractDtos.Requests;
using BusinessLogic.DTOs.ContractDtos.Responses;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Repositories.Interfaces
{
    public interface IContractRepository
    {
        Task<PagedResult<ContractResponseDto>> GetContractsByOwnerIdAsync(Guid ownerId, SieveModel sieveModel);
        public Task<ContractResponseDto?> GetContractByIdAsync(Guid contractId);
        public Task<IList<ContractResponseDto>?> GetContractsByTenantIdAsync(Guid tenantId);
        public Task<IList<ContractResponseDto>?> GetContractsByApartmentIdAsync(Guid apartmentId);
        public Task<bool> UpdateContractAsync(Guid contractId, Contract contract);
        Task<decimal> GetTotalPaymentsSumByContractId(Guid contractId);
            public Task<bool> AddPaymentRequestAsync(Guid contractId, PaymentLog payment);
        public Task<PaymentLog?> GetPaymentByIdAsync(Guid paymentId);
        public Task<IList<PaymentLog>?> GetPaymentsByContractIdAsync(Guid contractId);
        public Task<bool> UpdatePaymentAsync(Guid paymentId, PaymentLog payment);
    }
}

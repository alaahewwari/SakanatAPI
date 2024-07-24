
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.ApartmentDtos.Responses;
using BusinessLogic.DTOs.ContractDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace BusinessLogic.Repositories.Implementations
{
    public class ContractRepository(
        ApplicationDbContext context,
        ISieveProcessor sieveProcessor,
        IMapper mapper) 
        : IContractRepository
    {
        public async Task<PagedResult<ContractResponseDto>> GetContractsByOwnerIdAsync(Guid ownerId,SieveModel sieveModel)
        {
            sieveModel.SetDefaultPagination();

            var contractsWQuery = context.Contracts
                .AsNoTracking()
                .Where(a => a.Apartment.UserId == ownerId);
            var filteredContracts = sieveProcessor.Apply(sieveModel, contractsWQuery, applyPagination: false);
            var totalCount = await filteredContracts.CountAsync();
            var paginatedContracts = sieveProcessor.Apply(sieveModel, contractsWQuery);
            var projectedContracts = await paginatedContracts
                .ProjectTo<ContractResponseDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return new PagedResult<ContractResponseDto>(
                projectedContracts,
                totalCount,
                sieveModel.Page!.Value,
                sieveModel.PageSize!.Value
            );
        }

        public async Task<IList<ContractResponseDto>?> GetContractsByTenantIdAsync(Guid tenantId)
        {
            var contracts = context.Contracts
                .AsNoTracking()
                .Where(c => c.TenantId == tenantId)
                .ProjectTo<ContractResponseDto>(mapper.ConfigurationProvider);
            return await contracts.ToListAsync();
        }
        public async Task<ContractResponseDto?> GetContractByIdAsync(Guid contractId)
        {
            var contract =await context.Contracts.AsNoTracking()
                .Where(c => c.Id == contractId)
                .ProjectTo<ContractResponseDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();
            return contract;
        }
        public async Task<IList<ContractResponseDto>?> GetContractsByApartmentIdAsync(Guid apartmentId)
        {
            var contract = context.Contracts.AsNoTracking()
                .Where(c => c.ApartmentId == apartmentId)
                .ProjectTo<ContractResponseDto>(mapper.ConfigurationProvider);
            return await contract.ToListAsync();
        }
        public async Task<bool> UpdateContractAsync(Guid contractId, Contract contract)
        {
            var existingContract = await context.Contracts
                .FirstOrDefaultAsync(c => c.Id == contractId);
            if (existingContract is not null )
            {
                existingContract.StartDate = contract.StartDate;
                existingContract.EndDate = contract.EndDate;
                existingContract.RentPrice = contract.RentPrice;
existingContract.type = contract.type;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
public async Task<decimal> GetTotalPaymentsSumByContractId (Guid contractId)
        {
            var paymentsSum = await context.PaymentLogs
                .Where(p => p.ContractId == contractId)
                .SumAsync(p => p.Amount);
            return paymentsSum;
        }   
        public async Task<bool> AddPaymentRequestAsync(Guid contractId, PaymentLog payment)
        {
            var contract = await context.Contracts
                .FirstOrDefaultAsync(c => c.Id == contractId);

            ////calculate the sum of all payments amounts for the contract 
            //var paymentsSum =GetTotalPaymentsSumByContractId(contractId).Result;
            if (contract is not null)
            {
                payment.ContractId = contractId;
                context.PaymentLogs.Add(payment);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<PaymentLog?> GetPaymentByIdAsync(Guid paymentId)
        {
            var payment = await context.PaymentLogs
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == paymentId);
            return payment;
        }
        public async Task<IList<PaymentLog>?> GetPaymentsByContractIdAsync(Guid contractId)
        {
            var payments = await context.PaymentLogs
                .AsNoTracking()
                .Where(p => p.ContractId == contractId)
                .ToListAsync();
            return payments;
        }
        public async Task<bool> UpdatePaymentAsync(Guid paymentId, PaymentLog payment)
        {
            var existingPayment = await context.PaymentLogs
                .FirstOrDefaultAsync(p => p.Id == paymentId);
            if (existingPayment is not null)
            {
                existingPayment.Amount = payment.Amount;
                existingPayment.Date = payment.Date;
                existingPayment.ContractId = payment.ContractId;
                context.PaymentLogs.Update(existingPayment);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}

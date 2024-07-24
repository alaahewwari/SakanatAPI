

using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Common.Models;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.Extensions;
using BusinessLogic.Repositories.Interfaces;
using DataAccess;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace BusinessLogic.Repositories.Implementations;

public class SuspensionRepository(
    ApplicationDbContext context,
    IMapper mapper,
    ISieveProcessor sieveProcessor)
    : ISuspensionRepository
{
    public async Task CreateSuspensionAsync(Suspension suspension)
    {
        suspension.StartDate = DateOnly.FromDateTime(DateTime.UtcNow);
        await context.Suspensions.AddAsync(suspension);
        await context.SaveChangesAsync();
    }
    public async Task UnSuspendUserAsync(Guid userId)
    {
        var suspension = await context.Suspensions
            .Where(s => s.UserId == userId)
            .FirstOrDefaultAsync();

        if (suspension is not null)
        {
            context.Suspensions.Remove(suspension);
            await context.SaveChangesAsync();
        }
    }

    public async Task<PagedResult<GetSuspendedUserResponseDto>> GetAllSuspendedUsersAsync(SieveModel sieveModel)
    {
        sieveModel.SetDefaultPagination();
        var suspensionsQuery =context.Suspensions.AsNoTracking();

        var filteredSuspensions = sieveProcessor.Apply(sieveModel, suspensionsQuery, applyPagination: false);
        var totalCount = await filteredSuspensions.CountAsync();

        var paginatedSuspensions = sieveProcessor.Apply(sieveModel, suspensionsQuery);
        var projectedSuspensions = await paginatedSuspensions
            .ProjectTo<GetSuspendedUserResponseDto>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new PagedResult<GetSuspendedUserResponseDto>(
            projectedSuspensions,
            totalCount,
            sieveModel.Page!.Value,
            sieveModel.PageSize!.Value
        );
    }

    public async Task<Suspension?> GetSuspendedUserByIdAsync(Guid userId)
    {
        var suspension = await context.Suspensions.AsNoTracking()
            .Where(s => s.UserId == userId)
            .FirstOrDefaultAsync();
        return suspension;
    }

    public async Task<IList<string>?> GetSuspensionReasonsAsync()
    {
        var reasons = await context.Suspensions
                            .Select(s => s.Reason)
                            .Distinct()
                            .ToListAsync();
        return reasons;
    }
}
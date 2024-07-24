using AutoMapper;
using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.DTOs.UserDtos.Responses;
using BusinessLogic.ErrorHandling;
using BusinessLogic.Repositories.Interfaces;
using BusinessLogic.Services.IdentityServices.Interfaces;
using BusinessLogic.Services.UserServices.Interfaces;
using DataAccess.Models;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.UserServices.Implementations;

public class SuspensionServices(
    IIdentityManager identityManager,
    ISuspensionRepository suspensionRepository,
    IMapper mapper)
    : ISuspensionServices
{
    public async Task<ErrorOr<SuccessResponse>> SuspendUserAsync(SuspendUserRequestDto request)
    {
        var user = await identityManager.FindByIdAsync(request.UserId);
        if (user is null)
        {
            return Errors.Identity.UserNotFound;
        }
        var isSuspended = await suspensionRepository.GetSuspendedUserByIdAsync(request.UserId);
        if (isSuspended is not null)
        {
            return Errors.Suspension.UserIsSuspended;
        }
        var suspension=mapper.Map<Suspension>(request);
        await suspensionRepository.CreateSuspensionAsync(suspension);
        return new SuccessResponse("User suspended successfully");
    }

    public async Task<ErrorOr<SuccessResponse>> UnSuspendUserAsync(Guid userId)
    {
        var user = await identityManager.FindByIdAsync(userId);
        if (user is null) 
            return Errors.Identity.UserNotFound;
        await suspensionRepository.UnSuspendUserAsync(userId);
        return new SuccessResponse("User unsuspended successfully");
    }

    public async Task<ErrorOr<PagedResult<GetSuspendedUserResponseDto>>> GetAllSuspendedUsersAsync(SieveModel sieveModel)
    {
        var suspensions = await suspensionRepository.GetAllSuspendedUsersAsync(sieveModel);
        return suspensions;
    }

    public async Task<ErrorOr<GetSuspendedUserResponseDto>> GetSuspendedUserByIdAsync(Guid userId)
    {
        var suspension = await suspensionRepository.GetSuspendedUserByIdAsync(userId);
        if (suspension is null)
        {
            return Errors.Suspension.UserNotSuspended;
        }
        var isSuspended = await suspensionRepository.GetSuspendedUserByIdAsync(suspension.UserId);
        var response = mapper.Map<GetSuspendedUserResponseDto>(suspension);
        return response;
    }

    public async Task<ErrorOr<IList<string>>> GetAllSuspensionReasonsAsync()
    {
        var reasons = await suspensionRepository.GetSuspensionReasonsAsync();
        if (reasons is null) 
            return Errors.Suspension.NoSuspensionReasons;
        var response = mapper.Map<List<string>>(reasons);
        return response;
    }
}
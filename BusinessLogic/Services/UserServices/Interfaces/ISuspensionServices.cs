using BusinessLogic.Common.Models;
using BusinessLogic.Contracts.General;
using BusinessLogic.DTOs.UserDtos.Requests;
using BusinessLogic.DTOs.UserDtos.Responses;
using ErrorOr;
using Sieve.Models;

namespace BusinessLogic.Services.UserServices.Interfaces;

public interface ISuspensionServices
{
    Task<ErrorOr<SuccessResponse>> SuspendUserAsync(SuspendUserRequestDto request);
    Task<ErrorOr<SuccessResponse>> UnSuspendUserAsync(Guid userId);
    Task<ErrorOr<PagedResult<GetSuspendedUserResponseDto>>> GetAllSuspendedUsersAsync(SieveModel sieveModel);
    Task<ErrorOr<GetSuspendedUserResponseDto>> GetSuspendedUserByIdAsync(Guid userId);
    Task<ErrorOr<IList<string>>> GetAllSuspensionReasonsAsync();
}
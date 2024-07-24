using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.UserDtos.Requests;

public record ChangePasswordRequestDto
{
    public string CurrentPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;

    [Compare("NewPassword")] public string ConfirmNewPassword { get; set; } = null!;
}
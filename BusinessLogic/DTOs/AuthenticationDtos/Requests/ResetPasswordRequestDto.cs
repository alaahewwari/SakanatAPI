using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.AuthenticationDtos.Requests;

public class ResetPasswordRequestDto
{
    [Required] public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "The password and configuration password do not match")]
    public string ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
}
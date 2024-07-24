namespace BusinessLogic.DTOs.AuthenticationDtos.Requests;

public record ForgetPasswordRequestDto
{
    public string Email { get; set; } = null!;
}
namespace BusinessLogic.DTOs.AuthenticationDtos.Responses;

public record VerifyEmailRequestDto
{
    public string Token { get; set; } 
    public string Email { get; set; }

}
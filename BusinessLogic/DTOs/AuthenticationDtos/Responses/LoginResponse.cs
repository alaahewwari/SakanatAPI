namespace BusinessLogic.DTOs.AuthenticationDtos.Responses;

public record LoginResponse
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiryDate { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryDate { get; set; }
}
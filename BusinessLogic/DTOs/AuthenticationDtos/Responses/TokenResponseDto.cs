using BusinessLogic.Infrastructure.Authentication;

namespace BusinessLogic.DTOs.AuthenticationDtos.Responses;

public class TokenResponseDto
{
    public JwtToken AccessToken { get; set; } = null!;
    public JwtToken RefreshToken { get; set; }
}
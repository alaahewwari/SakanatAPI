namespace Presentation.Responses;

public record AuthenticationResponse
{
    public required string Token { get; set; }
}
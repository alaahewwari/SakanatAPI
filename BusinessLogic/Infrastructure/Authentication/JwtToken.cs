namespace BusinessLogic.Infrastructure.Authentication;

public class JwtToken
{
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryTokenDate { get; set; }
}
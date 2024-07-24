using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.AuthenticationDtos.Requests;

public record UserRegistrationRequestDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string CityName { get; set; }
    public string? PhoneNumber { get; set; }
    public required string Role { get; set; }


}
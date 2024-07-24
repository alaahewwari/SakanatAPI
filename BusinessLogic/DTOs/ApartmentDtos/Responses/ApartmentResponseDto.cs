using BusinessLogic.DTOs.EnumDtos;
using BusinessLogic.DTOs.UserDtos.Responses;

namespace BusinessLogic.DTOs.ApartmentDtos.Responses;

public record ApartmentResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Region { get; set; }
    public string Building { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsVisible { get; set; }
    public bool IsDiscounted { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public DateOnly CreationDate { get; set; }
    public FurnishedStatusDto FurnishedStatus { get; set; }
    public GenderAllowedDto GenderAllowed { get; set; }
    public PriceCurrencyDto PriceCurrency { get; set; }
    public GetUserResponseDto User { get; set; }
    public string CityName { get; set; }
    public string UniversityName { get; set; }
}
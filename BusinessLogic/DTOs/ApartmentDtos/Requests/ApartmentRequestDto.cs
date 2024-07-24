using BusinessLogic.DTOs.CityDtos.Requests;
using BusinessLogic.DTOs.UniversityDtos.Requests;


namespace BusinessLogic.DTOs.ApartmentDtos.Requests;

public record ApartmentRequestDto
{
    public string Name { get; set; }
    public string Region { get; set; }
    public string Building { get; set; }
    public int FloorNumber { get; set; }
    public int ApartmentNumber { get; set; }
    public int NumberOfRooms { get; set; }
    public int NumberOfBathrooms { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public byte FurnishedStatus { get; set; }
    public byte GenderAllowed { get; set; }
    public byte PriceCurrency { get; set; }
    public string CityName { get; set; }
    public string UniversityName { get; set; }

}
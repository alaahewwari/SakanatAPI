namespace BusinessLogic.DTOs.CityDtos.Responses;

public record CityResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly CreationDate { get; set; }
    public int NumberOfApartments { get; set; }
}
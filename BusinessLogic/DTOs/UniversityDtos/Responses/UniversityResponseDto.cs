namespace BusinessLogic.Dtos.University;

public record UniversityResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateOnly CreationDate { get; set; }
    public int NumberOfApartments { get; set; }
}

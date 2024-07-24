
namespace BusinessLogic.DTOs.ApartmentDtos.Responses
{
    public record ApartmentImageResponseDto
    {
        public Guid Id { get; set; }
        public string ImagePath { get; set; }
        public bool IsCover { get; set; } = false;
        public Guid ApartmentId { get; set; }
    }
}

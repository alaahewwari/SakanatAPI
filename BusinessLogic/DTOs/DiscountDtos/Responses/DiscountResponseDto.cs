

namespace BusinessLogic.DTOs.DiscountDtos.Responses
{
    public class DiscountResponseDto
    {
        public Guid Id { get; init; }
        public string Description { get; init; }
        public double Percentage { get; init; }
        public DateOnly CreationDate { get; init; }
        public int ApartmentsCount { get; set; }
        public bool IsAdded { get; set; }

    }
}

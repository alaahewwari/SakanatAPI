namespace BusinessLogic.DTOs.DiscountDtos.Requests
{
    public record DiscountRequestDto
    {
        public string Description { get; init; }
        public double Percentage { get; init; }
    }
}

using BusinessLogic.DTOs.DiscountDtos.Responses;

namespace BusinessLogic.DTOs.ApartmentDtos.Responses
{
    public class ApartmentDiscountsResponseDto
    {
        public string ApartmentName { get; set; }
        public IList<DiscountResponseDto> Discounts { get; set; } = new List<DiscountResponseDto>();
        public int DiscountsCount { get; set; }
    }
}

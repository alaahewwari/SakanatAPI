

namespace BusinessLogic.DTOs.ContractDtos.Requests
{
    public class CreateContractRequestDto
    {
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
        public decimal RentPrice { get; set; }
        public byte Type { get; set; }
        public byte Currency { get; set; }
    }
}

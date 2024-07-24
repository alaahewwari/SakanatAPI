using BusinessLogic.DTOs.CityDtos.Responses;
using BusinessLogic.DTOs.ContractDtos.Responses;

namespace BusinessLogic.DTOs.TenantsDtos.Responses
{
    public class TenantResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Note { get; set; }
        public CityResponseDto City { get; set; }
        public DateOnly CreationDate { get; set; }
        public IList<ContractResponseDto> Contracts { get; set; } = new List<ContractResponseDto>();
    }
}

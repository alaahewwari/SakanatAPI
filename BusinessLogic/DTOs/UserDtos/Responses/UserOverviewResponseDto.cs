
namespace BusinessLogic.DTOs.UserDtos.Responses
{
    public class UserOverviewResponseDto
    {
        public Guid Id { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string? PhoneNumber { get; set; } 
        public DateOnly CreationDate { get; set; }
        public string RoleName { get; set; }
        public string ImagePath { get; set; }
    }
}

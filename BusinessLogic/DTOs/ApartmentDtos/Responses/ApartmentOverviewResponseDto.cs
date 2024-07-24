using BusinessLogic.DTOs.UserDtos.Responses;

namespace BusinessLogic.DTOs.ApartmentDtos.Responses
{
    public class ApartmentOverviewResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string Building { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsVisible { get; set; }
        public bool IsDiscounted { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public int Price { get; set; }
        public string PriceCurrency { get; set; } 
        public DateOnly CreationDate { get; set; }
        public GetUserResponseDto User { get; set; }
        public string CityName { get; set; }
        public string UniversityName { get; set; }
    }
}


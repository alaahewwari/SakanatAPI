namespace BusinessLogic.DTOs.ApartmentDtos.Requests
{
    public record ApartmentToUpdateRequestDto
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public int NumberOfRooms { get; set; }
        public int NumberOfBathrooms { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public byte FurnishedStatus { get; set; }
        public byte GenderAllowed { get; set; }
        public byte PriceCurrency { get; set; }
    }
}

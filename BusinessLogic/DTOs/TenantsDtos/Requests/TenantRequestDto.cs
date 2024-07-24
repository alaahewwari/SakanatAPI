namespace BusinessLogic.DTOs.TenantsDtos.Requests
{
    public class TenantRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CityId { get; set; }
        public string Note { get; set; }
    }
}
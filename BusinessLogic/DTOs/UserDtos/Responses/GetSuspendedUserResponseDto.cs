namespace BusinessLogic.DTOs.UserDtos.Responses;

public class GetSuspendedUserResponseDto
{
    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public string Reason { get; set; }
}
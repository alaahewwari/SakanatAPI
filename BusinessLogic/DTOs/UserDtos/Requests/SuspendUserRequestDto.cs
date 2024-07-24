namespace BusinessLogic.DTOs.UserDtos.Requests;

public class SuspendUserRequestDto
{
    public Guid UserId { get; set; }
    public Guid ApartmentId { get; set; }
    public DateOnly EndDate { get; set; }
    public string Reason { get; set; }
}
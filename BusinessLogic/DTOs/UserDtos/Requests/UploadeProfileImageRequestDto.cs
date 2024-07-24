using Microsoft.AspNetCore.Http;

namespace BusinessLogic.DTOs.UserDtos.Requests;

public class UploadeProfileImageRequestDto
{
    public IFormFile ImageFile { get; set; } = null!;
}
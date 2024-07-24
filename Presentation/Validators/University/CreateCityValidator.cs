using BusinessLogic.DTOs.UniversityDtos.Requests;
using FluentValidation;

namespace Presentation.Validators.University;

public class CreateUniversityValidator : AbstractValidator<UniversityRequestDto>
{
    public CreateUniversityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("University name is required");
    }
}
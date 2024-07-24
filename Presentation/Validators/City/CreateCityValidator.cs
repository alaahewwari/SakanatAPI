using BusinessLogic.DTOs.CityDtos.Requests;
using FluentValidation;

namespace Presentation.Validators.City;

public class CreateCityValidator : AbstractValidator<CityRequestDto>
{
    public CreateCityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("City name is required");
    }
}
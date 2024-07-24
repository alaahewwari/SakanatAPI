using BusinessLogic.DTOs.ApartmentDtos.Requests;
using FluentValidation;
namespace Presentation.Validators.Apartment;

public class CreateApartmentValidator : AbstractValidator<ApartmentRequestDto>
{
    public CreateApartmentValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Apartment name is required")
            .MaximumLength(30).WithMessage("Apartment name must be at most 15 characters");
        RuleFor(c => c.Description)
            .NotEmpty().WithMessage("Apartment description is required")
            .MinimumLength(20).WithMessage("Apartment description must be at least 20 characters");
        RuleFor(c => c.Building)
            .NotEmpty().WithMessage("Building name is required")
            .MaximumLength(15).WithMessage("Building name must be at most 50 characters");
        RuleFor(c => c.CityName)
            .NotEmpty().WithMessage("City name is required");
        RuleFor(c => c.UniversityName)
            .NotEmpty().WithMessage("University name is required");
        RuleFor(c => c.Price)
            .NotEmpty().WithMessage("Apartment price is required");
        RuleFor(c => c.FurnishedStatus)
            .NotEmpty().WithMessage("Furnished status name is required");
        RuleFor(c => c.PriceCurrency)
            .NotEmpty().WithMessage("Price currency name is required");
        RuleFor(c => c.ApartmentNumber)
            .NotEmpty().WithMessage("Apartment number name is required");
        RuleFor(c => c.FloorNumber)
            .NotEmpty().WithMessage("Floor number name is required");
        RuleFor(c => c.NumberOfRooms)
            .NotEmpty().WithMessage("Number of rooms name is required");
        RuleFor(c => c.NumberOfBathrooms)
            .NotEmpty().WithMessage("Number of bathrooms name is required");
    }
}
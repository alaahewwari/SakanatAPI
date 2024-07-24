using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using FluentValidation;
using Presentation.Common.Validation;

namespace Presentation.Validators.Identity;

public class UserLoginValidator : AbstractValidator<UserLoginRequestDto>
{
    public UserLoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(6).WithMessage("Password must be at least 8 characters long");
    }
}
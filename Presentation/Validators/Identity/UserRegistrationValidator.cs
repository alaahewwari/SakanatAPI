using BusinessLogic.DTOs.AuthenticationDtos.Requests;
using FluentValidation;
using Presentation.Common.Validation;

namespace Presentation.Validators.Identity
{
    public class UserRegistrationValidator: AbstractValidator<UserRegistrationRequestDto>, IValidatorMarker
    {
        public UserRegistrationValidator()
        {
            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters.")
                .MinimumLength(2).WithMessage("First name must be at least 2 characters.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("First name must contain only letters.");

            RuleFor(user=>user.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters.")
                .MinimumLength(2).WithMessage("Last name must be at least 2 characters.")
                .Matches(@"^[a-zA-Z]+$").WithMessage("Last name must contain only letters")
                .Must((basicInfo, firstName) => basicInfo.LastName != basicInfo.FirstName)
                .WithMessage("First name and last name cannot be the same.");


            RuleFor(user=>user.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Email address is not valid.")
                .MaximumLength(50).WithMessage("Email address must not exceed 50 characters.");

            RuleFor(user => user.Password)
                .Must(password => password.Any(char.IsUpper))
                .WithMessage("Password must contain at least one uppercase letter.")
                .Must(password => password.Any(char.IsLower))
                .WithMessage("Password must contain at least one lowercase letter.")
                .Must(password => password.Any(char.IsDigit))
                .WithMessage("Password must contain at least one digit.");


        }
    }
}

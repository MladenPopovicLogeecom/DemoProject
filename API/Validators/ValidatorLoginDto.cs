using API.DTOs.UserDTOs;
using FluentValidation;

namespace API.Validators;

public class ValidatorLoginDto : AbstractValidator<LoginDto>
{
    public ValidatorLoginDto()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(4, 20).WithMessage("Username must be between 4 and 20 characters");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(1, 50).WithMessage("Code cannot be longer than 50 characters.");
        
    }
}
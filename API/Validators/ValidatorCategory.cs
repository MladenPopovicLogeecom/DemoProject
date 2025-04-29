using API.DTOs.CategoryDTOs;
using FluentValidation;

namespace API.Validators;

public class ValidatorCategory : AbstractValidator<CategoryDto>
{
    public ValidatorCategory()
    {
        
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(1, 100).WithMessage("Title cannot be longer than 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(1, 50).WithMessage("Code cannot be longer than 50 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot be longer than 500 characters.");
        

        

        
    }
    
}
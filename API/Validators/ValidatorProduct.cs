using API.DTOs.ProductDTOs;
using FluentValidation;

namespace API.Validators;

public class ProductValidator : AbstractValidator<ProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("SKU is required.")
            .Length(3, 50).WithMessage("SKU must be between 3 and 50 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(4, 100).WithMessage("Title must be between 4 and 100 characters.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand is required.")
            .Length(2, 50).WithMessage("Brand must be between 2 and 50 characters.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("CategoryId is required.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");

        RuleFor(x => x.ShortDescription)
            .MaximumLength(200).WithMessage("Short description cannot be longer than 200 characters.")
            .When(x => !string.IsNullOrEmpty(x.ShortDescription));

        RuleFor(x => x.LongDescription)
            .MaximumLength(1000).WithMessage("Long description cannot be longer than 1000 characters.")
            .When(x => !string.IsNullOrEmpty(x.LongDescription));

        RuleFor(x => x.IsEnabled)
            .NotNull().WithMessage("IsEnabled must be specified.");

        RuleFor(x => x.IsFeatured)
            .NotNull().WithMessage("IsFeatured must be specified.");
    }
}
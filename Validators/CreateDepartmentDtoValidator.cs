using FluentValidation;
using prac.Models;

namespace prac.Validators;

public class CreateDepartmentDtoValidator : AbstractValidator<CreateDepartmentDto>
{
    public CreateDepartmentDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Department name is required.")
            .Length(2, 100).WithMessage("Department name must be between 2 and 100 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Department code is required.")
            .Length(2, 10).WithMessage("Department code must be between 2 and 10 characters.")
            .Matches("^[A-Z0-9]+$").WithMessage("Department code must contain only uppercase letters and numbers.");

        RuleFor(x => x.Building)
            .NotEmpty().WithMessage("Building location is required.")
            .MaximumLength(100).WithMessage("Building name cannot exceed 100 characters.");
    }
}

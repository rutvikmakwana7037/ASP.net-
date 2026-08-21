using FluentValidation;
using prac.Models;

namespace prac.Validators;

public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(x => x.RollNumber)
            .NotEmpty().WithMessage("Roll number is required.")
            .Matches(@"^STU-\d{3,6}$").WithMessage("Roll number must follow the format 'STU-101' or 'STU-00100'.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("A valid email address is required.");

        RuleFor(x => x.Age)
            .InclusiveBetween(16, 100).WithMessage("Age must be between 16 and 100.");

        RuleFor(x => x.Gpa)
            .InclusiveBetween(0.0, 4.0).WithMessage("GPA must be between 0.0 and 4.0.");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Valid DepartmentId is required.");
    }
}

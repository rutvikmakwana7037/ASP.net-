using FluentValidation;
using prac.Models;

namespace prac.Validators;

public class CreateCourseDtoValidator : AbstractValidator<CreateCourseDto>
{
    public CreateCourseDtoValidator()
    {
        RuleFor(x => x.CourseCode)
            .NotEmpty().WithMessage("Course code is required.")
            .Length(3, 20).WithMessage("Course code must be between 3 and 20 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Course title is required.")
            .Length(3, 150).WithMessage("Course title must be between 3 and 150 characters.");

        RuleFor(x => x.Credits)
            .InclusiveBetween(1, 10).WithMessage("Credits must be between 1 and 10.");

        RuleFor(x => x.DepartmentId)
            .GreaterThan(0).WithMessage("Valid DepartmentId is required.");
    }
}

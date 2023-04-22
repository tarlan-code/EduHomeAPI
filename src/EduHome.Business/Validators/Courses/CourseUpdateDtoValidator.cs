using EduHome.Business.DTOs.Courses;
using FluentValidation;

namespace EduHome.Business.Validators.Courses;
public class CourseUpdateDtoValidator : AbstractValidator<CourseUpdateDto>
{
    public CourseUpdateDtoValidator()
    {
        RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Id is required!")
            .NotNull().WithMessage("Id is required!");
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is required!")
            .NotNull().WithMessage("Name is required!")
            .Length(3, 150).WithMessage("Length 3-150 symbol");
        RuleFor(c => c.Description).Length(5, 500).WithMessage("Length 5-500 symbol");
        RuleFor(c => c.Description).Length(5, 500).WithMessage("Length 5-500 symbol");
    }
}
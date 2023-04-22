using EduHome.Business.DTOs.Auth;
using EduHome.Business.DTOs.Courses;
using FluentValidation;

namespace EduHome.Business.Validators.Auth;
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(c => c.Fullname)
            .NotEmpty().WithMessage("Fullnam is required!")
            .NotNull().WithMessage("Fullnam is required!")
            .Length(3, 50).WithMessage("Length 3-50 symbol");
        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("Email is required")
            .NotNull().WithMessage("Fullnam is required!")
            .EmailAddress();
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required")
            .NotNull().WithMessage("Password is required!")
            .MinimumLength(3);
        RuleFor(c => c.ConfirmPassword)
            .Equal(c => c.Password)
            .NotEmpty().WithMessage("ConfirmPassword is required")
            .NotNull().WithMessage("ConfirmPassword is required!");

       
    }
}


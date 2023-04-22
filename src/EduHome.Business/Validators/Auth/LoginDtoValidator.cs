using EduHome.Business.DTOs.Auth;
using FluentValidation;

namespace EduHome.Business.Validators.Auth;
public class LoginDtoValidator: AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(c => c.Username)
            .NotEmpty().WithMessage("Username is required!")
            .NotNull().WithMessage("Username is required!")
            .Length(3, 50).WithMessage("Length 3-50 symbol");
        RuleFor(c => c.Password)
            .NotEmpty().WithMessage("Password is required")
            .NotNull().WithMessage("Password is required!")
            .MinimumLength(3);
       


    }
}

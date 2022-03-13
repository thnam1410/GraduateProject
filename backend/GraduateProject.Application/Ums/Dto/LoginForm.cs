using FluentValidation;

namespace GraduateProject.Application.Ums.Dto;

public class LoginForm
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public bool RememberMe = false;
}

public class ValidateLoginForm : AbstractValidator<LoginForm>
{
    public ValidateLoginForm()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(5);
        RuleFor(x => x.Password).NotNull().NotEmpty().MinimumLength(5);
    }
}
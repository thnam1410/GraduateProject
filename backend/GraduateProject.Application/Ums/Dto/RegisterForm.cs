using FluentValidation;

namespace GraduateProject.Application.Ums.Dto;

public class RegisterForm
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FullName { get; set; }
}

public class ValidateRegisterForm : AbstractValidator<RegisterForm>
{
    public ValidateRegisterForm()
    {
        RuleFor(x => x.UserName).Null().NotEmpty().MinimumLength(6);
        RuleFor(x => x.Password).Null().NotEmpty();
        RuleFor(x => x.ConfirmPassword).Null().NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches("/(84|0[3|5|7|8|9])+([0-9]{8})\b/g"); // vietnamese phone regex 
    }
}
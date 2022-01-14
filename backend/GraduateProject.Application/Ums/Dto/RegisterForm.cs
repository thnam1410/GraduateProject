using FluentValidation;

namespace GraduateProject.Application.Ums.Dto;

public class RegisterForm
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string? FullName { get; set; }
}

public class ValidateRegisterForm : AbstractValidator<RegisterForm>
{
    public ValidateRegisterForm()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty().MinimumLength(5).WithMessage("Contain at least 5 characters");
        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("Password empty!");
        RuleFor(x => x.ConfirmPassword).NotNull().NotEmpty().WithMessage("Confirm password empty!");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email");
        RuleFor(x => x.PhoneNumber).NotEmpty().NotNull().Matches("(84|0[3|5|7|8|9])+([0-9]{8})").WithMessage("Invalid phonenumber"); // vietnamese phone regex
        //or ([\\+84|84|0]+(3|5|7|8|9|1[2|6|8|9]))+([0-9]{8})
    }
}
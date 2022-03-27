using GraduateProject.Application.Extensions;

namespace GraduateProject.Application.Core;

public class InvalidBusinessException: Exception
{
    public bool IsDeferException { private set; get; } = false;

    public ICoreValidationResult ValidateResult { get; set; }

    public InvalidBusinessException(string message)
        : base(message)
    {
    }

    public InvalidBusinessException(ICoreValidationResult validationResult)
        : base("Invalid BusinessException!")
    {
        this.IsDeferException = true;
        this.ValidateResult = validationResult;
    }
}
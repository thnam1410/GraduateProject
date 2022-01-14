using GraduateProject.Application.Extensions;

namespace GraduateProject.Application.Common.Dto;

public class ValidateFailure : IValidateFailure
{
    public string PropertyName { get; set; }

    public string ErrorMessage { get; set; }

    public object AttemptedValue { get; set; }

    public string ErrorCode { get; set; }

    public ValidateFailure(string propertyName, string errorMessage, object attemptedValue = null, string errorCode = null)
    {
        this.PropertyName = propertyName;
        this.ErrorMessage = errorMessage;
        this.AttemptedValue = attemptedValue;
        this.ErrorCode = errorCode;
    }
}
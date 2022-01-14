using FluentValidation;
using FluentValidation.Results;
using GraduateProject.Application.Common.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace GraduateProject.Application.Extensions;

public class BaseValidator : IValidator
{
    private readonly IServiceProvider _serviceProvider;

    public BaseValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public bool Validate<T>(T obj, out ICoreValidationResult result) where T : class
    {
        if ((object) obj != null)
        {
            result = (ICoreValidationResult) new CoreValidationResult();
            ValidationResult validatedResult = this._serviceProvider.GetRequiredService<IValidator<T>>().Validate(obj);
            if (!validatedResult.IsValid)
                result = this.GetValidationResult(validatedResult);
            return validatedResult.IsValid;
        }

        result = (ICoreValidationResult) new CoreValidationResult();
        return false;
    }

    private ICoreValidationResult GetValidationResult(
        ValidationResult validatedResult)
    {
        CoreValidationResult validationResult = new CoreValidationResult();
        foreach (ValidationFailure error in (IEnumerable<ValidationFailure>) validatedResult.Errors)
            validationResult.Errors.Add(new ValidateFailure(BaseValidator.ToCamelCase(error.PropertyName), error.ErrorMessage,
                error.AttemptedValue, error.ErrorCode));
        return validationResult;
    }

    private static string ToCamelCase(string propertyName) => string.Join(".",
        (propertyName.Split(".")).Select((Func<string, string>) (x => char.ToLower(x[0]) + x.Substring(1))));
}
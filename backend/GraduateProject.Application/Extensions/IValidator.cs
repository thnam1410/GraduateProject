namespace GraduateProject.Application.Extensions;

public interface IValidator
{
    bool Validate<T>(T obj, out ICoreValidationResult results) where T : class;
}

public interface ICoreValidationResult
{
    IList<IValidateFailure> Errors { get; set; }

    bool IsValid { get; }
}

public interface IValidateFailure
{
    string PropertyName { get; set; }

    string ErrorMessage { get; set; }

    object AttemptedValue { get; set; }

    string ErrorCode { get; set; }
}
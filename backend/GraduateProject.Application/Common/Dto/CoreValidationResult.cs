using GraduateProject.Application.Extensions;

namespace GraduateProject.Application.Common.Dto;

public class CoreValidationResult : ICoreValidationResult
{
    public IList<IValidateFailure> Errors { get; set; }

    public bool IsValid => this.Errors.Count == 0;

    public CoreValidationResult() => this.Errors = (IList<IValidateFailure>) new List<IValidateFailure>();
}
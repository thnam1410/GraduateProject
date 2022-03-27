using FluentValidation;

namespace GraduateProject.Application.RealEstate.MasterDataDto;

public class MasterDataCreateDto
{
    public Guid? ParentId { get; set; }
    public string MasterKey { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class ValidateMasterDataCreateDto : AbstractValidator<MasterDataCreateDto>
{
    public ValidateMasterDataCreateDto()
    {
        RuleFor(x => x.MasterKey).NotNull();
        RuleFor(x => x.Code).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
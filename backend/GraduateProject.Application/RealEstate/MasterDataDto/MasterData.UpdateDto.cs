using FluentValidation;

namespace GraduateProject.Application.RealEstate.MasterDataDto;

public class MasterDataUpdateDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string MasterKey { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}

public class ValidateMasterDataUpdateDto : AbstractValidator<MasterDataUpdateDto>
{
    public ValidateMasterDataUpdateDto()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.MasterKey).NotNull();
        RuleFor(x => x.Code).NotNull();
        RuleFor(x => x.Name).NotNull();
    }
}
﻿namespace GraduateProject.Application.RealEstate.MasterDataDto;

public class MasterDataDetailDto
{
    public Guid Id { get; set; }
    public Guid? ParentId { get; set; }
    public string MasterKey { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
}
using GraduateProject.Domain.Common;
using GraduateProject.Domain.Constants;

namespace GraduateProject.Domain.AppEntities.Entities;

public class RealEstate : TrackableEntity<Guid>
{
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? Description { get; set; }
    public string Address { get; set; }
    public int Area { get; set; }
    public long Price { get; set; }
    public int BedroomCount { get; set; }
    public int ToiletCount { get; set; }
    public int FloorCount { get; set; }
    public bool IsFrontLine { get; set; }
    public HomeDirection Direction { get; set; }
    public Guid? AvatarAttachFileId { get; set; }
    public Guid? PicturesAttachFileId { get; set; }
    public string? YoutubeUrl { get; set; }
    public string? MapLocationJSON { get; set; }
    
    public Guid? ProjectId { get; set; }
    public Project Project { get; set; }
    public MasterData Country { get; set; }
    public Guid CountryId { get; set; }
    public MasterData District { get; set; }
    public Guid DistrictId { get; set; }
    public MasterData Ward { get; set; }
    public Guid WardId { get; set; }
    public MasterData PriceType { get; set; }
    public Guid PriceTypeId { get; set; }
    public MasterData ServiceType { get; set; }
    public Guid ServiceTypeId { get; set; }
}
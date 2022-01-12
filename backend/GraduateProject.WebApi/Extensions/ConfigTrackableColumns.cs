using GraduateProject.Domain.Common;
using GraduateProject.Domain.Common.Auditing;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraduateProject.Extensions;

public static class ConfigTrackableColumns
{
    public static void ConfigTrackableColumnOrder<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder)
        where TEntity : class, IFullTrackableItem
    {
        entityTypeBuilder.Property(x => x.CreatedBy).HasMaxLength(255);
        entityTypeBuilder.Property(x => x.CreatedTime);
        entityTypeBuilder.Property(x => x.LastModifiedBy).HasMaxLength(255);
        entityTypeBuilder.Property(x => x.LastModifiedTime);
    }
}
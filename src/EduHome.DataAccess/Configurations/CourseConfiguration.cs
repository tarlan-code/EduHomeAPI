using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHome.DataAccess.Configurations;
public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
   

    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.Property(x => x.Name).IsRequired(true).HasMaxLength(50);
        builder.Property(X => X.Description).IsRequired(false).HasMaxLength(500);
        builder.Property(X => X.Description).IsRequired(false).HasMaxLength(500);
    }
}

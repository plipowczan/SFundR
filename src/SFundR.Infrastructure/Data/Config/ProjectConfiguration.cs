using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFundR.Core.ProjectAggregate;

namespace SFundR.Infrastructure.Data.Config;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
  public void Configure(EntityTypeBuilder<Project> builder)
  {
    builder.Property(p => p.Name)
      .HasMaxLength(100)
      .IsRequired();

    builder.Property(p => p.Description)
      .HasMaxLength(512);
  }
}

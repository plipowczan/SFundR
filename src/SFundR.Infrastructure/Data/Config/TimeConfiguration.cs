using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SFundR.Core.ProjectAggregate;

namespace SFundR.Infrastructure.Data.Config;

public class TimeConfiguration : IEntityTypeConfiguration<TimeItem>
{
  public void Configure(EntityTypeBuilder<TimeItem> builder)
  {
    builder.Property(t => t.Comment)
      .IsRequired();
  }
}

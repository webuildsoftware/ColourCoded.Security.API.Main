using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColourCoded.Security.API.Data.Entities.Permission
{
  public class ArtifactMapping : IEntityTypeConfiguration<Artifact>
  {
    public void Configure(EntityTypeBuilder<Artifact> builder)
    {
      builder.ToTable("Artifacts");

      builder.HasKey("ArtifactId");

      builder.HasMany(m => m.Permissions);
    }
  }
}

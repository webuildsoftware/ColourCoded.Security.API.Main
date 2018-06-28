using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColourCoded.Security.API.Data.Entities.Role
{
  public class RoleMapping : IEntityTypeConfiguration<Role>
  {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.ToTable("Roles");

      builder.HasKey("RoleId");

      builder.HasMany(m => m.RoleMembers);
    }
  }
}

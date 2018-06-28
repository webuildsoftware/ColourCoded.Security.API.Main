using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColourCoded.Security.API.Data.Entities.Permission
{
  public class PermissionMapping : IEntityTypeConfiguration<Permission>
  {
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
      builder.ToTable("Permissions");

      builder.HasKey("PermissionId");
    }
  }
}
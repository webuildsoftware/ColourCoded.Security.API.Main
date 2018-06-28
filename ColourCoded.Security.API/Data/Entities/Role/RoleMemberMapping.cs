using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColourCoded.Security.API.Data.Entities.Role
{
  public class RoleMemberMapping : IEntityTypeConfiguration<RoleMember>
  {
    public void Configure(EntityTypeBuilder<RoleMember> builder)
    {
      builder.ToTable("RoleMembers");

      builder.HasKey("RoleMemberId");
    }
  }
}
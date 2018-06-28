using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColourCoded.Security.API.Data.Entities.Login
{
  public class SessionMapping : IEntityTypeConfiguration<Session>
  {
    public void Configure(EntityTypeBuilder<Session> builder)
    {
      builder.ToTable("Sessions");

      builder.HasKey("SessionId");
    }
  }
}

using Microsoft.EntityFrameworkCore;
using ColourCoded.Security.API.Data.Entities.Role;
using ColourCoded.Security.API.Data.Entities.Login;
using ColourCoded.Security.API.Data.Entities.Permission;

namespace ColourCoded.Security.API.Data
{
  public class SecurityContext : DbContext
  {
    public DbSet<Role> Roles { get; set; }
    public DbSet<Artifact> Artifacts { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Salt> Salts { get; set; }

    public SecurityContext(DbContextOptions<SecurityContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.ApplyConfiguration(new RoleMapping());
      modelBuilder.ApplyConfiguration(new RoleMemberMapping());
      modelBuilder.ApplyConfiguration(new ArtifactMapping());
      modelBuilder.ApplyConfiguration(new PermissionMapping());
      modelBuilder.ApplyConfiguration(new UserMapping());
      modelBuilder.ApplyConfiguration(new SaltMapping());
      modelBuilder.ApplyConfiguration(new SessionMapping());
    }
  }
}

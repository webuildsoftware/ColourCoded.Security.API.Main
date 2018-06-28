using System;

namespace ColourCoded.Security.API.Data.Entities.Permission
{
  public class Permission
  {
    public int PermissionId { get; set; }
    public int ArtifactId { get; set; }
    public int RoleId { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUser { get; set; }
  }
}

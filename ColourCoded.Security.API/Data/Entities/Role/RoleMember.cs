using System;

namespace ColourCoded.Security.API.Data.Entities.Role
{
  public class RoleMember
  {
    public int RoleMemberId { get; set; }

    public int RoleId { get; set; }

    public string Username { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateUser { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string UpdateUser { get; set; }
  }
}

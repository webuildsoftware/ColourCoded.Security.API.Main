using System;
using System.Collections.Generic;

namespace ColourCoded.Security.API.Data.Entities.Role
{
  public class Role
  {
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UpdateUser { get; set; }
    public List<RoleMember> RoleMembers { get; set; }

    public Role()
    {
      RoleMembers = new List<RoleMember>();
    }
  }
}

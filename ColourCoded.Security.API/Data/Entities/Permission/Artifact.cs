using System;
using System.Collections.Generic;

namespace ColourCoded.Security.API.Data.Entities.Permission
{
  public class Artifact
  {
    public int ArtifactId { get; set; }
    public string ArtifactName { get; set; }
    public DateTime CreateDate { get; set; }
    public string CreateUser { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string UpdateUser { get; set; }
    public List<Permission> Permissions { get; set; }

    public Artifact()
    {
      Permissions = new List<Permission>();
    }
  }
}

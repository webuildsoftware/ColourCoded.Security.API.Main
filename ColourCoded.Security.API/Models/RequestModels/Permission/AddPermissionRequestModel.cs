namespace ColourCoded.Security.API.Models.RequestModels.Permission
{
  public class AddPermissionRequestModel
  {
    public int ArtifactId { get; set; }
    public int RoleId { get; set; }
    public string CreateUser { get; set; }
  }
}

namespace ColourCoded.Security.API.Models.RequestModels.Role
{
  public class AddRoleMemberRequestModel
  {
    public int RoleId { get; set; }
    public string Username { get; set; }
    public string CreateUser { get; set; }
  }
}

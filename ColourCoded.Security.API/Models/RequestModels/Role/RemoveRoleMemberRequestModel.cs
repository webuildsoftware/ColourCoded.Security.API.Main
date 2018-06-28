namespace ColourCoded.Security.API.Models.RequestModels.Role
{
  public class RemoveRoleMemberRequestModel
  {
    public int RoleId { get; set; }

    public int RoleMemberId { get; set; }
  }
}

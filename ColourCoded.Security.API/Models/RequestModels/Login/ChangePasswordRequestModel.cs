namespace ColourCoded.Security.API.Models.RequestModels.Login
{
  public class ChangePasswordRequestModel
  {
    public string Username { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
  }
}

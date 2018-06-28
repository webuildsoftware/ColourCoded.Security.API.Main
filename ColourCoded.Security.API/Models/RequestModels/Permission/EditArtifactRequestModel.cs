namespace ColourCoded.Security.API.Models.RequestModels.Permission
{
  public class EditArtifactRequestModel
  {
    public int ArtifactId { get; set; }
    public string ArtifactName { get; set; }
    public string CreateUser { get; set; }
  }
}

using Microsoft.AspNetCore.Mvc;
using ColourCoded.Security.API.Data;
using ColourCoded.Security.API.Models.ResponseModels;
using System.Collections.Generic;
using System.Linq;
using ColourCoded.Security.API.Models.RequestModels.Permission;
using ColourCoded.Security.API.Shared;
using ColourCoded.Security.API.Data.Entities.Permission;
using System;
using Microsoft.EntityFrameworkCore;

namespace ColourCoded.Security.API.Controllers
{
  [Route("api/permissions")]
  public class PermissionsController : Controller
  {
    protected SecurityContext Context;

    public PermissionsController(SecurityContext context)
    {
      Context = context;
    }

    [HttpPost, Route("artifacts/getall")]
    public List<ArtifactModel> GetAll()
    {
      return Context.Artifacts.Select(r => new ArtifactModel { ArtifactId = r.ArtifactId, ArtifactName = r.ArtifactName }).ToList();
    }

    [HttpPost, Route("artifacts/add")]
    public ValidationResult AddArtifact([FromBody]AddArtifactRequestModel requestModel)
    {
      var response = new ValidationResult();

      var existingArtifact = Context.Artifacts.FirstOrDefault(r => r.ArtifactName.ToUpper() == requestModel.ArtifactName.ToUpper());

      if (existingArtifact != null)
      {
        response.InValidate("", "The artifact already exists");
        return response;
      }

      var artifact = new Artifact
      {
        ArtifactName = requestModel.ArtifactName,
        CreateDate = DateTime.Now,
        CreateUser = requestModel.CreateUser
      };

      Context.Artifacts.Add(artifact);
      Context.SaveChanges();

      return response;
    }

    [HttpPost, Route("artifacts/edit")]
    public ValidationResult EditArtifact([FromBody]EditArtifactRequestModel requestModel)
    {
      var response = new ValidationResult();

      var existingArtifact = Context.Artifacts.FirstOrDefault(r => r.ArtifactId == requestModel.ArtifactId);

      if (existingArtifact == null)
      {
        response.InValidate("", "The artifact does not exist");
        return response;
      }

      existingArtifact.ArtifactName = requestModel.ArtifactName;
      existingArtifact.UpdateDate = DateTime.Now;
      existingArtifact.UpdateUser = requestModel.CreateUser;
      Context.SaveChanges();

      return response;
    }

    [HttpPost, Route("artifacts/remove")]
    public void RemoveArtifact([FromBody]RemoveArtifactRequestModel requestModel)
    {
      var existingArtifact = Context.Artifacts.Include(r => r.Permissions).FirstOrDefault(r => r.ArtifactId == requestModel.ArtifactId);

      if (existingArtifact != null)
      {
        Context.Artifacts.Remove(existingArtifact);
        Context.SaveChanges();
      }
    }

    [HttpPost, Route("getall")]
    public List<PermissionModel> GetAllPermissions([FromBody]FindPermissionsRequestModel requestModel)
    {
      var permissions = new List<PermissionModel>();
      var existingArtifact = Context.Artifacts.Include(r => r.Permissions).FirstOrDefault(r => r.ArtifactId == requestModel.ArtifactId);

      if (existingArtifact == null)
        return permissions;

      foreach (var permission in existingArtifact.Permissions)
      {
        var role = Context.Roles.FirstOrDefault(r => r.RoleId == permission.RoleId);

        if (role == null)
          continue;

        permissions.Add(new PermissionModel
        {
          ArtifactId = existingArtifact.ArtifactId,
          PermissionId = permission.PermissionId,
          RoleName = role.RoleName
        });
      }

      return permissions;
    }

    [HttpPost, Route("add")]
    public ValidationResult AddPermission([FromBody]AddPermissionRequestModel requestModel)
    {
      var response = new ValidationResult();

      var existingArtifact = Context.Artifacts.Include(r => r.Permissions).FirstOrDefault(r => r.ArtifactId == requestModel.ArtifactId);

      if (existingArtifact == null)
      {
        response.InValidate("", "The artifact does not exist");
        return response;
      }

      if (existingArtifact.Permissions.FirstOrDefault(r => r.RoleId == requestModel.RoleId) != null)
      {
        response.InValidate("", "The permission already exists");
        return response;
      }

      var permission = new Permission
      {
        ArtifactId = requestModel.ArtifactId,
        RoleId = requestModel.RoleId,
        CreateDate = DateTime.Now,
        CreateUser = requestModel.CreateUser
      };

      existingArtifact.Permissions.Add(permission);
      Context.SaveChanges();

      return response;
    }

    [HttpPost, Route("remove")]
    public void RemovePermission([FromBody]RemovePermissionRequestModel requestModel)
    {
      var existingArtifact = Context.Artifacts.Include(r => r.Permissions).FirstOrDefault(r => r.ArtifactId == requestModel.ArtifactId);

      if (existingArtifact != null)
      {
        existingArtifact.Permissions.RemoveAll(r => r.PermissionId == requestModel.PermissionId);
        Context.SaveChanges();
      }
    }
  }
}

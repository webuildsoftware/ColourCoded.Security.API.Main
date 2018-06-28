using Microsoft.AspNetCore.Mvc;
using ColourCoded.Security.API.Data;
using ColourCoded.Security.API.Models.ResponseModels;
using System.Collections.Generic;
using System.Linq;
using ColourCoded.Security.API.Models.RequestModels.Role;
using ColourCoded.Security.API.Shared;
using ColourCoded.Security.API.Data.Entities.Role;
using System;
using Microsoft.EntityFrameworkCore;

namespace ColourCoded.Security.API.Controllers
{
  [Route("api/roles")]
  public class RolesController : Controller
  {
    protected SecurityContext Context;

    public RolesController(SecurityContext context)
    {
      Context = context;
    }

    [HttpGet, Route("test")]
    public List<RoleModel> Test()
    {
      return Context.Roles.Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName }).ToList();
    }

    [HttpPost, Route("getall")]
    public List<RoleModel> GetAll()
    {
      return Context.Roles.Select(r => new RoleModel { RoleId = r.RoleId, RoleName = r.RoleName }).ToList();
    }

    [HttpPost, Route("searchusers")]
    public List<string> SearchUsers([FromBody]SearchUsersRequestModel requestModel)
    {
      return Context.Users.Where(u => u.Username.Contains(requestModel.SearchTerm)).Select(u => u.Username).ToList();
    }

    [HttpPost, Route("add")]
    public ValidationResult AddRole([FromBody]AddRoleRequestModel requestModel)
    {
      var response = new ValidationResult();

      var role = new Role {
        RoleName = requestModel.RoleName,
        CreateDate = DateTime.Now,
        CreateUser = requestModel.CreateUser
      };

      var existingRole = Context.Roles.FirstOrDefault(r => r.RoleName.ToUpper() == role.RoleName.ToUpper());

      if(existingRole != null)
      {
        response.InValidate("", "The role already exists");
        return response;
      }

      Context.Roles.Add(role);
      Context.SaveChanges();

      return response;
    }

    [HttpPost, Route("edit")]
    public ValidationResult EditRole([FromBody]EditRoleRequestModel requestModel)
    {
      var response = new ValidationResult();

      var existingRole = Context.Roles.FirstOrDefault(r => r.RoleId == requestModel.RoleId);

      if (existingRole == null)
      {
        response.InValidate("", "The role does not exist");
        return response;
      }

      existingRole.RoleName = requestModel.RoleName;
      existingRole.UpdateDate = DateTime.Now;
      existingRole.UpdateUser = requestModel.CreateUser;
      Context.SaveChanges();

      return response;
    }

    [HttpPost, Route("remove")]
    public void RemoveRole([FromBody]RemoveRoleRequestModel requestModel)
    {
      var existingRole = Context.Roles.Include(r => r.RoleMembers).FirstOrDefault(r => r.RoleId == requestModel.RoleId);

      if (existingRole != null)
      {
        Context.Roles.Remove(existingRole);
        Context.SaveChanges();
      }
    }

    [HttpPost, Route("rolemembers/getall")]
    public List<RoleMemberModel> GetAllMembers([FromBody]FindRoleMembersRequestModel requestModel)
    {
      return Context.Roles
        .Include(r => r.RoleMembers)
        .FirstOrDefault(r => r.RoleId == requestModel.RoleId)
        .RoleMembers.Select(r => new RoleMemberModel { RoleId = r.RoleId, Username = r.Username, RoleMemberId = r.RoleMemberId }).ToList();
    }

    [HttpPost, Route("rolemembers/add")]
    public ValidationResult AddRoleMember([FromBody]AddRoleMemberRequestModel requestModel)
    {
      var response = new ValidationResult();

      var existingRole = Context.Roles.Include(r => r.RoleMembers).FirstOrDefault(r => r.RoleId == requestModel.RoleId);

      if (existingRole == null)
      {
        response.InValidate("", "The role does not exist");
        return response;
      }

      if (existingRole.RoleMembers.FirstOrDefault(r => r.Username.ToUpper() == requestModel.Username.ToUpper()) != null)
      {
        response.InValidate("", "The rolemember already exists");
        return response;
      }

      var rolemember = new RoleMember
      {
        RoleId = requestModel.RoleId,
        Username = requestModel.Username,
        CreateDate = DateTime.Now,
        CreateUser = requestModel.CreateUser
      };

      existingRole.RoleMembers.Add(rolemember);
      Context.SaveChanges();

      return response;
    }

    [HttpPost, Route("rolemembers/remove")]
    public void RemoveRoleMember([FromBody]RemoveRoleMemberRequestModel requestModel)
    {
      var existingRole = Context.Roles.Include(r => r.RoleMembers).FirstOrDefault(r => r.RoleId == requestModel.RoleId);

      if (existingRole != null)
      {
        existingRole.RoleMembers.RemoveAll(r => r.RoleMemberId == requestModel.RoleMemberId);
        Context.SaveChanges();
      }
    }
  }
}

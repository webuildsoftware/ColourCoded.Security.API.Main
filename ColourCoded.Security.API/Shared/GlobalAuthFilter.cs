﻿using System;
using System.Linq;
using System.Net;
using ColourCoded.Security.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace ColourCoded.Security.API.Shared
{
  public class GlobalAuthFilter : IAuthorizationFilter
  {
    protected IMemoryCache MemoryCache;
    protected IConfiguration Configuration;
    protected SecurityContext Context;

    public GlobalAuthFilter(SecurityContext context, IMemoryCache memoryCache, IConfiguration configuration)
    {
      MemoryCache = memoryCache;
      Context = context;
      Configuration = configuration;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
      if (context.Filters != null)
      {
        if (context.Filters.Any(x => x.GetType() == typeof(IgnoreOAth)))
          return;
      }

      var sessionToken = context.HttpContext.Request.Query["token"].ToString();

      if (MemoryCache.Get(sessionToken) == null)
      {
        // check if session exists in context that is not expired
        var session = Context.Sessions.FirstOrDefault(s => s.Token.ToUpper() == sessionToken.ToUpper() && s.ExpirationDate > DateTime.Now);

        if (session != null)
        {
          // save the session in the memory cache
          MemoryCache.Set(session.Token, session.Username,
          new MemoryCacheEntryOptions
          {
            AbsoluteExpiration = new DateTimeOffset(DateTime.Now.AddMinutes(Convert.ToInt32(Configuration["Session.Expiration"]))),
            SlidingExpiration = new TimeSpan(0, Convert.ToInt32(Configuration["Session.Timeout"]), 0)
          });

          return;
        }

        context.Result = new JsonResult(new Exception("Access Denied!"));
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
      }

    }
  }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ColourCoded.Security.API.Data;
using ColourCoded.Security.API.Shared;
using Microsoft.AspNetCore.Http;

namespace ColourCoded.Security.API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc(options => 
      {
        options.Filters.Add(typeof(GlobalExceptionFilter));
        options.Filters.Add(typeof(GlobalAuthFilter));
      });

      services.AddHttpsRedirection(options =>
      {
        options.RedirectStatusCode = StatusCodes.Status301MovedPermanently;
        options.HttpsPort = 443;
      });

      services.AddDbContext<SecurityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ColourCoded_Security_OLTP")));

      services.AddTransient<ISecurityHelper, SecurityHelper>();
      services.AddTransient<ICommunicationsHelper, CommunicationsHelper>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseMvc();
    }
  }
}

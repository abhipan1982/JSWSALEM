using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;
using PE.BaseDbEntity.PEContext;
using PE.DbEntity.EnumClasses;
using PE.DbEntity.PEContext;
using PE.HMIWWW.Core.Authorization;
using PE.HMIWWW.Core.Features;
using PE.HMIWWW.Core.Resources;
using PE.HMIWWW.Core.Signalr.Hubs;
using PE.HMIWWW.Services;
using SMF.DbEntity.Models;
using SMF.HMI.Core;
using SMF.HMIWWW.Attributes;
using SMF.HMIWWW.UnitConverter;
using SMF.Startup;

namespace PE.HMIWWW
{
  public class Startup
  {
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
      Configuration = configuration;
      Env = env;
    }

    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllersWithViews();

      var mvcBuilder = services.AddMvc(options =>
        {
          // This pushes users to login if not authenticated
          options.Filters.Add(new AuthorizeFilter());
        })
        .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

      if (Env.IsDevelopment())
      {
        mvcBuilder.AddRazorRuntimeCompilation();
      }

      services.AddBundles(Env.WebRootPath);

      services
        .AddSignalR(o =>
        {
          o.EnableDetailedErrors = true;
          o.MaximumReceiveMessageSize = 49152;
        }).AddJsonProtocol(options =>
        {
          options.PayloadSerializerOptions.PropertyNamingPolicy = null;
        });

      services.AddDbContext<PEContext>(options =>
        options.UseSqlServer(
          Configuration.GetConnectionString("PEContext")));

      services.AddDbContext<HmiContext>(options =>
       options.UseSqlServer(
         Configuration.GetConnectionString("PEContext")));

      services.AddDbContext<SMFContext>(options =>
        options.UseSqlServer(
          Configuration.GetConnectionString("SMFContext")));

      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(
          Configuration.GetConnectionString("SMFContext")));

      services.AddDbContext<TransferContext>(options =>
        options.UseSqlServer(
          Configuration.GetConnectionString("TransferContext")));

      services.AddDbContext<DWContext>(options =>
        options.UseSqlServer(
          Configuration.GetConnectionString("PEContext")));

      services.AddAuthorization(options =>
      {
        options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
        options.AddPolicy("PrimetalsOnly", policy => policy.RequireClaim("primetals"));
      });

      services.AddIdentity<ApplicationUser, IdentityRole>(options =>
      {
        options.Stores.MaxLengthForKeys = 128;
        options.Password = new PasswordOptions
        {
          RequiredLength = 8,
          RequireNonAlphanumeric = false,
          RequireDigit = false,
          RequireLowercase = false,
          RequireUppercase = false
        };
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.User.RequireUniqueEmail = true;
      }).AddDefaultTokenProviders()
      .AddEntityFrameworkStores<ApplicationDbContext>()
      .AddClaimsPrincipalFactory<CustomClaimFactory>();

      services.ConfigureApplicationCookie(options =>
      {
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Account/Login";
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
      });

      services.Configure<RequestLocalizationOptions>(options =>
      {
        CultureInfo[] supportedCultures = CultureHelper.GetLanguagesList()
          .Select(x => new CultureInfo(x.LanguageCode, false))
          .ToArray();

        //options.DefaultRequestCulture = new RequestCulture(CultureHelper.GetDefaultCulture()); commented by AP on 26052023
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.RequestCultureProviders.Clear();
        options.RequestCultureProviders.Add(new CookieRequestCultureProvider());
      });

      services.AddHttpContextAccessor();
      services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
      //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

      services.Configure<RazorViewEngineOptions>(o =>
      {
        o.ViewLocationFormats.Clear();
        o.ViewLocationFormats.Add
          ("~/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
        o.ViewLocationFormats.Add
          ("~/Views/Module/PE.Lite/{1}/{0}" + RazorViewEngine.ViewExtension);
        o.ViewLocationFormats.Add
          ("~/Views/System/{1}/{0}" + RazorViewEngine.ViewExtension);
      });

      Initialization.RegisterServices(services);
      //OpcLicenseHelper.Init(); commented by AP on 26052023
      services.AddKendo();
      services.AddMiniProfiler(options =>
      {
        options.IgnoredPaths.Add("/hmihub");
        options.IgnoredPaths.Add("/bundles");
      }).AddEntityFramework();

      services.AddFeatureManagement()
        .AddFeatureFilter<ClaimsFeatureFilter>();

      EnumInitializator.Init();
      UnitConverterHelper.Init(VM_Resources.ResourceManager);
      SmfAttributeInitializator.Init(VM_Resources.ResourceManager);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      //app.UseHttpsRedirection();
      
      app.UseWebOptimizer();
      app.UseDefaultFiles();
      app.UseStaticFiles();

      RequestLocalizationOptions localizationOptions =
        app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>().Value;
      app.UseRequestLocalization(localizationOptions);

      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();

      app.UseForFeature(FeatureFlags.Ui.Profiler, app => app.UseMiniProfiler());

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllerRoute("areas", "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        endpoints.MapControllerRoute(
          "default",
          "{controller=Home}/{action=Index}/{id?}");

        endpoints.MapHub<HmiHub>("/hmihub");
      });
    }
  }
}

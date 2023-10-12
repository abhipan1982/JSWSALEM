using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PE.HMIWWW.Core.Extensions;
using Serilog;
using Serilog.Events;

namespace PE.HMIWWW
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .CreateBootstrapLogger();

      try
      {
        Log.Information("Starting web host");
        CreateHostBuilder(args).Build()
          .Initialize()
          .Run();
      }
      catch (Exception ex)
      {
        Log.Fatal(ex, "Host terminated unexpectedly");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
      return Host.CreateDefaultBuilder(args)
        .UseSerilog((context, services, configuration) => configuration
          .ReadFrom.Configuration(context.Configuration)
          .ReadFrom.Services(services)
          .Enrich.FromLogContext())
        .ConfigureWebHostDefaults(webBuilder =>
        {
          webBuilder.UseWebRoot("wwwroot");
          webBuilder.UseStartup<Startup>();
        });
    }
  }
}

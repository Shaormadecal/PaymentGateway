using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.IO;

namespace PaymentGateway.WebApi
{
    class Program
    {
        public static IConfiguration Configuration { get; private set; }
        public static int Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            try
            {
                Log.Information("Starting payment gateway api");
                BuildWebHost(args).Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
         WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseConfiguration(Configuration)
        .UseSerilog()
        .Build();
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

namespace Muharaunda.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    //.WriteTo.File("C:\\Africa\\LogFile\\Munharaunda\\Log.txt")
            //    .CreateLogger();

            try
            {
                //Log.Information("Application starting up");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                //Log.CloseAndFlush();
            }

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                //.UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

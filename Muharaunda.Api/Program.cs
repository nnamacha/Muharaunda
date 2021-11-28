using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Munharaunda.Api.Extensions;
using System;

namespace Muharaunda.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {




            try
            {
                ;

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception)
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
.ConfigureAppConfiguration((context, config) =>
{
    var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("KEYVAULT_URL"));
    config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
})
                .ConfigureAppConfiguration((context, config) =>
                {
                    if (context.HostingEnvironment.IsProduction())
                        config.ConfigureKeyVault();
                    else
                        config.WriteConfigurationSources();
                    var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("KEYVAULT_URL"));
                    config.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

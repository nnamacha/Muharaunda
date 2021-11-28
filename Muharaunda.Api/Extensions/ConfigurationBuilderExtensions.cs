using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munharaunda.Api.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static void ConfigureKeyVault(this IConfigurationBuilder config)
        {
            var keyVaultEndPoint = Environment.GetEnvironmentVariable("KEYVAULT_URL");

            if (keyVaultEndPoint is null)
            {
                throw new InvalidOperationException("Store the key Vault endpoint in a KEYVAULT_ENDPOINT environment variable");
            }

            var secretClient = new SecretClient(new(keyVaultEndPoint), new DefaultAzureCredential());
            config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());

        }
        public static void WriteConfigurationSources(this IConfigurationBuilder config)
        {
            Console.WriteLine("Configuration sources\n=====================");
            foreach (var source in config.Sources)
            {
                if (source is JsonConfigurationSource jsonSource)
                    Console.WriteLine($"{source}: {jsonSource.Path}");
                else
                    Console.WriteLine(source.ToString());
            }
            Console.WriteLine("=====================\n");
        }
    }
}

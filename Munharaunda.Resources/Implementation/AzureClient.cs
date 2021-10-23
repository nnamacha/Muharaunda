using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Threading.Tasks;

namespace Munharaunda.Infrastructure.Implementation
{
    public class AzureClient
    {
        public AzureClient()
        {

            // ...

        }


        public async Task Trying()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            string accessToken = await azureServiceTokenProvider.GetAccessTokenAsync("https://vault.azure.net");
            // OR
         
            var identityConnectionString1 = Environment.GetEnvironmentVariable("UA1_ConnectionString");
            var azureServiceTokenProvider1 = new AzureServiceTokenProvider(identityConnectionString1);

            var identityConnectionString2 = Environment.GetEnvironmentVariable("UA2_ConnectionString");
            var azureServiceTokenProvider2 = new AzureServiceTokenProvider(identityConnectionString2);
        }



    }
}

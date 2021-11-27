using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Munharaunda.Functions
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public async static Task Run([CosmosDBTrigger(
            databaseName: "munharaunda",
            collectionName: "Funeral",
            ConnectionStringSetting = "CosmosConnectionString",
            LeaseCollectionName = "leases",
            CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> input,
            
            ILogger log)
        {

            try
            {
                if (input != null && input.Count > 0)
                {
                    log.LogInformation("Documents modified " + input.Count);
                    log.LogInformation("First document Id " + input[0].Id);
                }

                var clientId = Environment.GetEnvironmentVariable("ClientId");

                var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");

                var adCredential = new ClientCredential(clientId,clientSecret);

                var authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/0100e0fe-36ff-433b-b61b-32efc3e98392");

                var token = await authenticationContext.AcquireTokenAsync(clientId, adCredential);

                var accessToken = token.AccessToken;
                
                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync("https://localhost:44347/api/profile");




            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}

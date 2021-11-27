using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Munharaunda.Functions
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                //if (input != null && input.Count > 0)
                //{
                //    log.LogInformation("Documents modified " + input.Count);
                //    log.LogInformation("First document Id " + input[0].Id);
                //}

                var clientId = Environment.GetEnvironmentVariable("ClientId");

                var clientSecret = Environment.GetEnvironmentVariable("ClientSecret");

                var adCredential = new ClientCredential(clientId, clientSecret);

                var authenticationContext = new AuthenticationContext("https://login.microsoftonline.com/a53164c8-ecce-43cd-9e56-2bf2d4a416be");

                var token = await authenticationContext.AcquireTokenAsync(clientId, adCredential);

                var accessToken = token.AccessToken;

                var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await httpClient.GetAsync("https://localhost:44347/api/profile");

                return new OkObjectResult("");
                {

                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

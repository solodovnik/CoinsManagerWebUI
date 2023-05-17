using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Authentication
{
    public class AzureJwtAuthenticator : IAuthenticator
    {
        private readonly IConfiguration _configuration;
        public AzureJwtAuthenticator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> GetToken()
        {
            var clientId = _configuration["Api:ClientId"];
            var clientSecret = _configuration["Api:ClientSecret"];
            var authority = _configuration["Api:Authority"];
            var applicationIdUri = _configuration["Api:ApplicationIdUri"];

            IConfidentialClientApplication app;
            app = ConfidentialClientApplicationBuilder.Create(clientId)
                .WithClientSecret(clientSecret)
                .WithAuthority(authority)
                .Build();

            var authResult = await app.AcquireTokenForClient(new string[] { $"{applicationIdUri}/.default" }).ExecuteAsync();
            return authResult.AccessToken;
        }
    }
}

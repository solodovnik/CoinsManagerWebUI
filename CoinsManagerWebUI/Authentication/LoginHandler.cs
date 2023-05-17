using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace CoinsManagerWebUI.Authentication
{
    public class LoginHandler : DelegatingHandler
    {
        private readonly IAuthenticator _authenticator;
        public LoginHandler(IAuthenticator authenticator)
        {
            _authenticator = authenticator;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _authenticator.GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}

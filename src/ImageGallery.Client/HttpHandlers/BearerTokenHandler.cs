using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using IdentityModel.Client;

namespace ImageGallery.Client.HttpHandlers
{
    public class BearerTokenHandler : DelegatingHandler
    {
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public BearerTokenHandler(IHttpContextAccessor httpContextAccessor) {
            this._httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
            CancellationToken cancellationToken) {

            var accessToken = await _httpContextAccessor
                .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                request.SetBearerToken(accessToken);
                request.Headers.Add("new-header", "custom header");
            }

            var response  =  await base.SendAsync(request, cancellationToken);

            response.Headers.Add("name", "name");

            foreach (var header in response.Headers)
            {
                var x = header.Key;
            }

            return response;
        }
    }
}

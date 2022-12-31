using API_Automation.Models.Response;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Automation.Auth
{
    public class APIAuthenticator : AuthenticatorBase
    {
        readonly string baseurl;
        readonly string clientId;
        readonly string clientSecret;

        public APIAuthenticator() : base("")
        {

        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        private async Task<string> GetToken()
        {
            var options = new RestClientOptions(baseurl);
            var client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(clientId, clientSecret)
            };

            var request = new RestRequest("oauth2/token")
                .AddParameter("grant type", "client_credentials");
            var response = await client.PostAsync<TokenResponse>(request);
            return $"{response.TokenType} {response.AccessToken}";
        }
    }
}

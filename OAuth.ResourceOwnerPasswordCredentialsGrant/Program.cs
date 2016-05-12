using System;
using System.Net.Http;
using Constants;
using DotNetOpenAuth.OAuth2;

namespace OAuth.ResourceOwnerPasswordCredentialsGrant
{
    internal class Program
    {
        private static WebServerClient _webServerClient;
        private static string _accessToken;

        private static void Main(string[] args)
        {
            InitializeWebServerClient();

            Console.WriteLine("要求授權Token...");
            RequestToken();
            Console.WriteLine("取得的授權Token: {0}", _accessToken);

            Console.WriteLine("存取受權限控管的WebApi資料...");
            var data = AccessProtectedResource();
            Console.WriteLine("會員資料為：{0}", data);
        }

        private static void InitializeWebServerClient()
        {
            var authorizationServerUri = new Uri(Paths.AuthorizationServerBaseAddress);
            var authorizationServer = new AuthorizationServerDescription
                                      {
                                          AuthorizationEndpoint = new Uri(authorizationServerUri, Paths.AuthorizePath),
                                          TokenEndpoint = new Uri(authorizationServerUri, Paths.TokenPath)
                                      };
            _webServerClient = new WebServerClient(authorizationServer, Clients.Client1.Id, Clients.Client1.Secret);
        }

        private static void RequestToken()
        {
            var state = _webServerClient.ExchangeUserCredentialForToken("User Name", "Password",
                new[] {"bio"});
            _accessToken = state.AccessToken;
        }

        private static string AccessProtectedResource()
        {
            var resourceServerUri = new Uri(Paths.ResourceServerBaseAddress);
            var client = new HttpClient(_webServerClient.CreateAuthorizingHandler(_accessToken));
            return client.GetStringAsync(new Uri(resourceServerUri, Paths.GrabMemberInfo)).Result;
        }
    }
}
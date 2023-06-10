namespace Gymby.ApiTests.Utils
{
    public class Authorization : IAuthorization
    {
        public async Task<string> GetAccessTokenAsync(string username, string password)
        {
            var identityServerUrl = "https://gymby-auth.azurewebsites.net/";
            var tokenEndpoint = $"{identityServerUrl}/connect/token";

            var clientId = "test";
            var clientSecret = "secret";
            var scope = "GymbyWebAPI";

            var httpClient = new HttpClient();

            var tokenClient = new TokenClient(httpClient, new TokenClientOptions
            {
                Address = tokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret
            });

            var client = await tokenClient.RequestPasswordTokenAsync(username, password, scope);

            return client.AccessToken!;
        }

        public static HttpClient GetAuthenticatedHttpClient(string accessToken)
        {
            var httpClient = new HttpClient();
            httpClient.SetBearerToken(accessToken);
            return httpClient;
        }
    }
}

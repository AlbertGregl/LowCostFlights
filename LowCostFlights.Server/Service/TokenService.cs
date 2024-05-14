using Newtonsoft.Json;

namespace LowCostFlights.Server.Service
{
    public class TokenService : ITokenService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public TokenService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<string> GetTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var key = _configuration["AmadeusAPI:ApiKey"];
            var secret = _configuration["AmadeusAPI:ApiSecret"];
            var api_url = _configuration["AmadeusAPI:ApiBaseUrl"] + _configuration["AmadeusAPI:ApiTokenUrl"];

            var content = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("grant_type", "client_credentials"),
            new KeyValuePair<string, string>("client_id", key),
            new KeyValuePair<string, string>("client_secret", secret)
        });

            var response = await client.PostAsync(api_url, content);
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                dynamic tokenData = JsonConvert.DeserializeObject(jsonContent);
                return tokenData.access_token;
            }
            else
            {
                throw new ApplicationException("Failed to retrieve access token.");
            }
        }
    }
}

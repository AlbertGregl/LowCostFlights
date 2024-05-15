using LowCostFlights.Server.ModelBL;
using LowCostFlights.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly ILogger<AirportsController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AirportsController(
            ILogger<AirportsController> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ITokenService tokenService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetAirports([FromQuery] AirportSearchRequest request)
        {
            _logger.LogInformation($"Searching for airports with keyword: {request.Keyword}");

            var client = _httpClientFactory.CreateClient();
            var token = await _tokenService.GetTokenAsync();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            string queryString = $"?subType={request.SubType}&keyword={request.Keyword}&page[limit]={request.Limit}&page[offset]={request.Offset}";

            var api_url = _configuration["AmadeusAPI:ApiBaseUrl"] + _configuration["AmadeusAPI:ApiAirportsUrl"];
            string fullUrl = api_url + queryString;

            try
            {
                var response = await client.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var airportResponse = JsonConvert.DeserializeObject<AirportSearchResponse>(jsonString);

                    if (airportResponse?.Airports == null || !airportResponse.Airports.Any())
                    {
                        _logger.LogWarning("No airports found or failed to deserialize data.");
                        return NotFound("No airports data available.");
                    }

                    return Ok(airportResponse.Airports);
                }
                _logger.LogWarning($"Failed to fetch airport data from Amadeus API with status code: {response.StatusCode}");
                return StatusCode((int)response.StatusCode, "Failed to fetch airport data from Amadeus API.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching airport data.");
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}

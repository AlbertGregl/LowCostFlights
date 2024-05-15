using LowCostFlights.Server.Mapping;
using LowCostFlights.Server.Model;
using LowCostFlights.Server.ModelBL;
using LowCostFlights.Server.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightOffersController : ControllerBase
    {
        private readonly ILogger<FlightOffersController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;


        public FlightOffersController(
            ILogger<FlightOffersController> logger, 
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
        public async Task<IActionResult> GetFlightOffers([FromQuery] FlightSearchRequest request)
        {
            _logger.LogInformation($"Fetching flight offers for {request.OriginLocationCode} to {request.DestinationLocationCode}");

            // Get the API token
            var token = await _tokenService.GetTokenAsync();

            var api_url = _configuration["AmadeusAPI:ApiBaseUrl"] + _configuration["AmadeusAPI:ApiFlightOffersUrl"];


            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            string queryString = $"?originLocationCode={request.OriginLocationCode}&destinationLocationCode={request.DestinationLocationCode}&departureDate={request.DepartureDate}&adults={request.Adults}&nonStop={request.NonStop}&currencyCode={request.CurrencyCode}&max={request.MaxNumberOfResults}";
            if (!string.IsNullOrEmpty(request.ReturnDate))
            {
                queryString += $"&returnDate={request.ReturnDate}";
            }

            string fullUrl = api_url + queryString;

            try
            {
                var response = await client.GetAsync(fullUrl);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var flightOffers = JsonConvert.DeserializeObject<FlightOffer>(jsonString);

                    if (flightOffers == null)
                    {
                        _logger.LogWarning("Failed to deserialize flight data.");
                        return NotFound("Flight data is unavailable.");
                    }

                    var mappedResponse = FlightOfferMapper.MapToResponse(flightOffers, request);

                    return Ok(mappedResponse);
                }
                _logger.LogWarning($"Failed to fetch data from Amadeus API with status code: {response.StatusCode}");
                return StatusCode((int)response.StatusCode, "Failed to fetch data from Amadeus API.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching flight offers.");
                return StatusCode(500, "An internal error occurred.");
            }
        }
    }
}

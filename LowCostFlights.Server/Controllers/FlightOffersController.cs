using LowCostFlights.Server.Mapping;
using LowCostFlights.Server.Model;
using LowCostFlights.Server.ModelBL;
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

        const string BASE_URL = "https://test.api.amadeus.com/v2/shopping/flight-offers";



        public FlightOffersController(ILogger<FlightOffersController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("/")]
        public IActionResult Get()
        {
            return Ok("Welcome to LowCostFlights API");
        }


        [HttpGet("get")]
        public async Task<IActionResult> GetFlightOffers([FromQuery] FlightSearchRequest request)
        {
            _logger.LogInformation($"Fetching flight offers for {request.OriginLocationCode} to {request.DestinationLocationCode}");

            var apiKey = _configuration["AmadeusAPI:ApiKey"];

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

            string queryString = $"?originLocationCode={request.OriginLocationCode}&destinationLocationCode={request.DestinationLocationCode}&departureDate={request.DepartureDate}&adults={request.Adults}&nonStop={request.NonStop}&currencyCode={request.CurrencyCode}&max={request.MaxNumberOfResults}";
            if (!string.IsNullOrEmpty(request.ReturnDate))
            {
                queryString += $"&returnDate={request.ReturnDate}";
            }

            string fullUrl = BASE_URL + queryString;

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

                    // sort the flights by departure date
                    //mappedResponse.Flights = mappedResponse.Flights.OrderBy(f => f.DepartureDate).ToList();

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

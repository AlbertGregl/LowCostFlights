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


        public FlightOffersController(ILogger<FlightOffersController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("test")]
        public IActionResult GetTestMockData()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "MockData", "flightOffersMock.json");
            try
            {
                var jsonText = System.IO.File.ReadAllText(filePath);
                var flightOffer = JsonConvert.DeserializeObject<FlightOffer>(jsonText);

                if (flightOffer == null || flightOffer.Data == null || !flightOffer.Data.Any())
                {
                    _logger.LogWarning("Failed to deserialize mock flight data or no data available.");
                    return NotFound("Mock flight data is unavailable.");
                }

                var request = new FlightSearchRequest
                {
                    OriginLocationCode = "LHR",
                    DestinationLocationCode = "CDG",
                    DepartureDate = "2022-01-01",
                    ReturnDate = "2022-01-07",
                    Adults = 1,
                    CurrencyCode = "EUR"
                };

                var response = FlightOfferMapper.MapToResponse(flightOffer, request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading or deserializing mock data");
                return StatusCode(500, "An internal error occurred.");
            }
        }


        [HttpGet(Name = "/")]
        public async Task<IActionResult> GetFlightOffers([FromQuery] FlightSearchRequest request)
        {
            _logger.LogInformation("Fetching flight offers for {Origin} to {Destination}", request.OriginLocationCode, request.DestinationLocationCode);

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "Your_Access_Token_Here");

            string baseUrl = "https://test.api.amadeus.com/v2/shopping/flight-offers";
            string queryString = $"?originLocationCode={request.OriginLocationCode}&destinationLocationCode={request.DestinationLocationCode}&departureDate={request.DepartureDate}&returnDate={request.ReturnDate}&adults={request.Adults}&nonStop={request.NonStop}&currencyCode={request.CurrencyCode}&max=5";
            string fullUrl = baseUrl + queryString;

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

using LowCostFlights.Server.Model;
using Newtonsoft.Json;

namespace LowCostFlights.Server.ModelBL
{
    public class AirportSearchResponse
    {
        [JsonProperty("data")]
        public List<Airport>? Airports { get; set; }
    }

}

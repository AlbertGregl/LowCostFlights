using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Airport
    {
        [JsonProperty("iataCode", NullValueHandling = NullValueHandling.Ignore)]
        public string? IataCode { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

    }

}

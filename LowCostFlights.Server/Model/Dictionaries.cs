using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Dictionaries
    {
        [JsonProperty("locations", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, Location>? Locations { get; set; }

        [JsonProperty("aircraft", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? Aircraft { get; set; }

        [JsonProperty("currencies", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? Currencies { get; set; }

        [JsonProperty("carriers", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, string>? Carriers { get; set; }
    }
}

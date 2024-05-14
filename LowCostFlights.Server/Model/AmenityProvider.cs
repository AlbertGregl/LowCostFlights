using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class AmenityProvider
    {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Name { get; set; }
    }
}

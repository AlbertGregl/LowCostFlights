using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Links
    {
        [JsonProperty("self", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Uri? Self { get; set; }
    }
}

using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class TravelerPricingPrice
    {
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Currency { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Total { get; set; }

        [JsonProperty("base", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Base { get; set; }
    }
}

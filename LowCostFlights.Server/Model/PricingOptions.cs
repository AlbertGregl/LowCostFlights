using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class PricingOptions
    {
        [JsonProperty("fareType", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string[]? FareType { get; set; }

        [JsonProperty("includedCheckedBagsOnly", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? IncludedCheckedBagsOnly { get; set; }
    }
}

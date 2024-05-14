using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Fee
    {
        [JsonProperty("amount", NullValueHandling = NullValueHandling.Ignore)]
        public virtual decimal? Amount { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Type { get; set; }
    }
}

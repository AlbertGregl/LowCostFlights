using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Meta
    {
        [JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? Count { get; set; }

        [JsonProperty("links", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Links? Links { get; set; }
    }
}

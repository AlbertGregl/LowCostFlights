using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class IncludedCheckedBags
    {
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? Quantity { get; set; }
    }
}

using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Operating
    {
        [JsonProperty("carrierCode", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? CarrierCode { get; set; }
    }
}

using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class SegmentAircraft
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Code { get; set; }
    }
}

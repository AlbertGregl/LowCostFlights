using LowCostFlights.Server.Converters;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Arrival
    {
        [JsonProperty("iataCode", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? IataCode { get; set; }

        [JsonProperty("terminal", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public virtual long? Terminal { get; set; }

        [JsonProperty("at", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? At { get; set; }
    }
}

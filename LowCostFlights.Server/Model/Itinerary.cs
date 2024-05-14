using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Itinerary
    {
        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Duration { get; set; }

        [JsonProperty("segments", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Segment[]? Segments { get; set; }
    }
}

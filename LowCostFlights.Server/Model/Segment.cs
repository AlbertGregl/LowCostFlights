using LowCostFlights.Server.Converters;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Segment
    {
        [JsonProperty("departure", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Arrival? Departure { get; set; }

        [JsonProperty("arrival", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Arrival? Arrival { get; set; }

        [JsonProperty("carrierCode", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? CarrierCode { get; set; }

        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public virtual long? Number { get; set; }

        [JsonProperty("aircraft", NullValueHandling = NullValueHandling.Ignore)]
        public virtual SegmentAircraft? Aircraft { get; set; }

        [JsonProperty("operating", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Operating? Operating { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Duration { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public virtual long? Id { get; set; }

        [JsonProperty("numberOfStops", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? NumberOfStops { get; set; }

        [JsonProperty("blacklistedInEU", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? BlacklistedInEU { get; set; }
    }
}

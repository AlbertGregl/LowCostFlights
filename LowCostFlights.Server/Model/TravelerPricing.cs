using LowCostFlights.Server.Converters;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class TravelerPricing
    {
        [JsonProperty("travelerId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public virtual long? TravelerId { get; set; }

        [JsonProperty("fareOption", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? FareOption { get; set; }

        [JsonProperty("travelerType", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? TravelerType { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public virtual TravelerPricingPrice? Price { get; set; }

        [JsonProperty("fareDetailsBySegment", NullValueHandling = NullValueHandling.Ignore)]
        public virtual FareDetailsBySegment[]? FareDetailsBySegment { get; set; }
    }
}

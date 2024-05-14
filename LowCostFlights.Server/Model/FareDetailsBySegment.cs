using LowCostFlights.Server.Converters;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class FareDetailsBySegment
    {
        [JsonProperty("segmentId", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public virtual long? SegmentId { get; set; }

        [JsonProperty("cabin", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Cabin { get; set; }

        [JsonProperty("fareBasis", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? FareBasis { get; set; }

        [JsonProperty("brandedFare", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? BrandedFare { get; set; }

        [JsonProperty("brandedFareLabel", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? BrandedFareLabel { get; set; }

        [JsonProperty("class", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Class { get; set; }

        [JsonProperty("includedCheckedBags", NullValueHandling = NullValueHandling.Ignore)]
        public virtual IncludedCheckedBags? IncludedCheckedBags { get; set; }

        [JsonProperty("amenities", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Amenity[]? Amenities { get; set; }
    }
}

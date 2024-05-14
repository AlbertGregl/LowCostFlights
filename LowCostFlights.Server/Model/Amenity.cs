using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Amenity
    {
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Description { get; set; }

        [JsonProperty("isChargeable", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? IsChargeable { get; set; }

        [JsonProperty("amenityType", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? AmenityType { get; set; }

        [JsonProperty("amenityProvider", NullValueHandling = NullValueHandling.Ignore)]
        public virtual AmenityProvider? AmenityProvider { get; set; }
    }
}

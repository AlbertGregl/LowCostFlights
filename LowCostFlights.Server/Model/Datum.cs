using LowCostFlights.Server.Converters;
using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Datum
    {
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Type { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public virtual long? Id { get; set; }

        [JsonProperty("source", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Source { get; set; }

        [JsonProperty("instantTicketingRequired", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? InstantTicketingRequired { get; set; }

        [JsonProperty("nonHomogeneous", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? NonHomogeneous { get; set; }

        [JsonProperty("oneWay", NullValueHandling = NullValueHandling.Ignore)]
        public virtual bool? OneWay { get; set; }

        [JsonProperty("lastTicketingDate", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? LastTicketingDate { get; set; }

        [JsonProperty("lastTicketingDateTime", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DateTimeOffset? LastTicketingDateTime { get; set; }

        [JsonProperty("numberOfBookableSeats", NullValueHandling = NullValueHandling.Ignore)]
        public virtual long? NumberOfBookableSeats { get; set; }

        [JsonProperty("itineraries", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Itinerary[]? Itineraries { get; set; }

        [JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
        public virtual DatumPrice? Price { get; set; }

        [JsonProperty("pricingOptions", NullValueHandling = NullValueHandling.Ignore)]
        public virtual PricingOptions? PricingOptions { get; set; }

        [JsonProperty("validatingAirlineCodes", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string[]? ValidatingAirlineCodes { get; set; }

        [JsonProperty("travelerPricings", NullValueHandling = NullValueHandling.Ignore)]
        public virtual TravelerPricing[]? TravelerPricings { get; set; }
    }
}

using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class Location
    {
        [JsonProperty("cityCode", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? CityCode { get; set; }

        [JsonProperty("countryCode", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? CountryCode { get; set; }
    }
}

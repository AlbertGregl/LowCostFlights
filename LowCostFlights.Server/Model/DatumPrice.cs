using Newtonsoft.Json;

namespace LowCostFlights.Server.Model
{
    public class DatumPrice
    {
        [JsonProperty("currency", NullValueHandling = NullValueHandling.Ignore)]
        public virtual string? Currency { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public virtual decimal? Total { get; set; }

        [JsonProperty("base", NullValueHandling = NullValueHandling.Ignore)]
        public virtual decimal? Base { get; set; }

        [JsonProperty("fees", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Fee[]? Fees { get; set; }

        [JsonProperty("grandTotal", NullValueHandling = NullValueHandling.Ignore)]
        public virtual decimal? GrandTotal { get; set; }
    }
}

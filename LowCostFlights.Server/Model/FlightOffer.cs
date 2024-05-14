using Newtonsoft.Json;


namespace LowCostFlights.Server.Model
{

    public class FlightOffer
    {
        [JsonProperty("meta", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Meta? Meta { get; set; }

        [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Datum[]? Data { get; set; }

        [JsonProperty("dictionaries", NullValueHandling = NullValueHandling.Ignore)]
        public virtual Dictionaries? Dictionaries { get; set; }
    }

}

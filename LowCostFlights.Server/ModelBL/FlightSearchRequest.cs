namespace LowCostFlights.Server.ModelBL
{
    public class FlightSearchRequest
    {
        public required string OriginLocationCode { get; set; }
        public required string DestinationLocationCode { get; set; }
        public required string DepartureDate { get; set; }
        public required string ReturnDate { get; set; }
        public int Adults { get; set; }
        public bool NonStop { get; set; }
        public required string CurrencyCode { get; set; }
    }
}

namespace LowCostFlights.Server.ModelBL
{
    public class FlightOfferResponse
    {
        public string? DepartureAirport { get; set; }
        public string? ArrivalAirport { get; set; }
        public string? DepartureDate { get; set; }
        public string? ReturnDate { get; set; }
        public int NumberOfStopsOutbound { get; set; }
        public int NumberOfStopsInbound { get; set; }
        public int NumberOfPassengers { get; set; }
        public string? Currency { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

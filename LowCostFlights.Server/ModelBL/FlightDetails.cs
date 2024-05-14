namespace LowCostFlights.Server.ModelBL
{
    public class FlightDetails
    {
        public required string DepartureAirport { get; set; }
        public required string ArrivalAirport { get; set; }
        public required string DepartureDate { get; set; }
        public required string ReturnDate { get; set; }
        public int NumberOfStopsOutbound { get; set; }
        public int NumberOfStopsInbound { get; set; }
        public int NumberOfPassengers { get; set; }
        public required string Currency { get; set; }
        public decimal TotalPrice { get; set; }
    }
}

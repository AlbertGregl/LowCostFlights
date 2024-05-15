namespace LowCostFlights.Server.ModelBL
{
    public class AirportSearchRequest
    {
        public required string Keyword { get; set; }
        public int Limit { get; set; } = 10;
        public int Offset { get; set; } = 0;
        public string SubType { get; set; } = "AIRPORT";
    }

}

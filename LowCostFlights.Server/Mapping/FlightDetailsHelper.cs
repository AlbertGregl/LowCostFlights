using LowCostFlights.Server.Model;
using LowCostFlights.Server.ModelBL;

namespace LowCostFlights.Server.Mapping
{
    public static class FlightDetailsHelper
    {
        public static FlightDetails CreateFlightDetailsFromSegment(Segment segment, Itinerary itinerary, int numberOfPassengers, string currency, decimal totalPrice, bool isOutbound)
        {
            var flightDetails = new FlightDetails
            {
                DepartureAirport = segment.Departure?.IataCode ?? "unknown port",
                ArrivalAirport = segment.Arrival?.IataCode ?? "unknown port",
                DepartureDate = segment.Departure?.At.ToString() ?? "unknown time",
                ReturnDate = segment.Arrival?.At.ToString() ?? "unknown time", 
                NumberOfPassengers = numberOfPassengers,
                Currency = currency,
                TotalPrice = totalPrice
            };

            if (isOutbound)
            {
                flightDetails.NumberOfStopsOutbound = itinerary.Segments.Length - 1;
            }
            else
            {
                flightDetails.NumberOfStopsInbound = itinerary.Segments.Length - 1;
            }

            return flightDetails;
        }
    }


}

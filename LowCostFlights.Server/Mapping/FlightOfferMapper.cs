using LowCostFlights.Server.ModelBL;
using LowCostFlights.Server.Model;

namespace LowCostFlights.Server.Mapping
{
    public static class FlightOfferMapper
    {
        public static FlightOfferResponse MapToResponse(FlightOffer flightOffer, FlightSearchRequest request)
        {
            var outboundItinerary = flightOffer.Data?.FirstOrDefault()?.Itineraries?.FirstOrDefault();
            var inboundItinerary = flightOffer.Data?.FirstOrDefault()?.Itineraries?.LastOrDefault();

            if (outboundItinerary == null || inboundItinerary == null)
            {
                throw new InvalidOperationException("Invalid itinerary data");
            }

            return new FlightOfferResponse
            {
                DepartureAirport = outboundItinerary.Segments?.FirstOrDefault()?.Departure?.IataCode ?? "Unknown",
                ArrivalAirport = inboundItinerary.Segments?.LastOrDefault()?.Arrival?.IataCode ?? "Unknown",
                DepartureDate = outboundItinerary.Segments?.FirstOrDefault()?.Departure?.At.ToString() ?? "Unknown date",
                ReturnDate = inboundItinerary.Segments?.LastOrDefault()?.Arrival?.At.ToString() ?? "Unknown date",
                NumberOfStopsOutbound = outboundItinerary.Segments?.Length - 1 ?? 0,
                NumberOfStopsInbound = inboundItinerary.Segments?.Length - 1 ?? 0,
                NumberOfPassengers = request.Adults,
                Currency = request.CurrencyCode,
                TotalPrice = flightOffer.Data?.FirstOrDefault()?.Price?.GrandTotal ?? 0
            };
        }
    }
}

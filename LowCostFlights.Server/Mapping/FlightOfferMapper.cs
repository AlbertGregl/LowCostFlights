using LowCostFlights.Server.ModelBL;
using LowCostFlights.Server.Model;
using System.Globalization;
namespace LowCostFlights.Server.Mapping
{
    public static class FlightOfferMapper
    {
        public static FlightOfferResponse MapToResponse(FlightOffer flightOffer, FlightSearchRequest request)
        {
            var outboundFlights = new List<FlightDetails>();
            var inboundFlights = new List<FlightDetails>();

            foreach (var datum in flightOffer.Data)
            {
                var outboundItinerary = datum.Itineraries.FirstOrDefault();
                var inboundItinerary = datum.Itineraries.LastOrDefault();

                // Process outbound flights
                if (outboundItinerary != null)
                {
                    foreach (var segment in outboundItinerary.Segments)
                    {
                        var flightDetails = FlightDetailsHelper.CreateFlightDetailsFromSegment(
                            segment,
                            outboundItinerary,
                            request.Adults,
                            request.CurrencyCode,
                            datum.Price?.GrandTotal ?? 0,
                            true // true indicates outbound
                        );
                        outboundFlights.Add(flightDetails);
                    }
                }

                // Process inbound flights
                if (inboundItinerary != null)
                {
                    foreach (var segment in inboundItinerary.Segments)
                    {
                        var flightDetails = FlightDetailsHelper.CreateFlightDetailsFromSegment(
                            segment,
                            inboundItinerary,
                            request.Adults,
                            request.CurrencyCode,
                            datum.Price?.GrandTotal ?? 0,
                            false // false indicates inbound
                        );
                        inboundFlights.Add(flightDetails);
                    }
                }
            }


            var combinedFlights = outboundFlights.Concat(inboundFlights)
                //.OrderBy(f => f.DepartureDate)
                .ToList();

            return new FlightOfferResponse { Flights = combinedFlights };
        }
    }
}
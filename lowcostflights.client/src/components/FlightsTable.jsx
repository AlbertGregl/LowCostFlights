import PropTypes from 'prop-types';

const FlightTable = ({ flights }) => {
    if (flights.length === 0) {
        return <p><em>&#9752;</em></p>;
    }

    return (
        <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Departure Airport</th>
                    <th>Arrival Airport</th>
                    <th>Departure Date</th>
                    <th>Return Date</th>
                    <th>No. of Stops Outbound</th>
                    <th>No. of Stops Inbound</th>
                    <th>No. of Passengers</th>
                    <th>Currency</th>
                    <th>Total Price</th>
                </tr>
            </thead>
            <tbody>
                {flights.map((flight, index) => (
                    <tr key={index}>
                        <td>{flight.departureAirport}</td>
                        <td>{flight.arrivalAirport}</td>
                        <td>{flight.departureDate}</td>
                        <td>{flight.returnDate}</td>
                        <td>{flight.numberOfStopsOutbound}</td>
                        <td>{flight.numberOfStopsInbound}</td>
                        <td>{flight.numberOfPassengers}</td>
                        <td>{flight.currency}</td>
                        <td>{flight.totalPrice}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
};

FlightTable.propTypes = {
    flights: PropTypes.array.isRequired
};

export default FlightTable;

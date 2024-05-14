import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [flights, setFlightOffers] = useState([]);

    useEffect(() => {
        populateFlightOffersData();
    }, []);

    const contents = flights.length === 0
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Departure Airport</th>
                    <th>Arrival Airport</th>
                    <th>Departure Date</th>
                    <th>ReturnDate</th>
                    <th>No. of Stops Outbound</th>
                    <th>No. of Stops Inbound</th>
                    <th>No. of Passengers</th>
                    <th>Currency</th>
                    <th>TotalPrice</th>
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
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Flight Offers</h1>
            <p>This component demonstrates fetching mock data from the server.</p>
            {contents}
        </div>
    );

    async function populateFlightOffersData() {
        try {
            const response = await fetch('api/flightoffers/test/');
            const data = await response.json();
            setFlightOffers(data.flights);
        } catch (error) {
            console.error('Failed to fetch data:', error);
        }
    }
}

export default App;

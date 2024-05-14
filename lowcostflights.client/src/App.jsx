//import { useEffect, useState } from 'react';
import { useState } from 'react';
import './App.css';

function App() {
    const [flights, setFlightOffers] = useState([]);
    const [searchParams, setSearchParams] = useState({
        originLocationCode: '',
        destinationLocationCode: '',
        departureDate: '',
        returnDate: '',
        adults: 1,
        nonStop: false,
        currencyCode: '',
        maxNumberOfResults: 5
    });

    //useEffect(() => {
    //    // TODO Call initial data load
        
    //}, []);

    const handleInputChange = (event) => {
        const { name, value, type, checked } = event.target;
        setSearchParams(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const formatDateForAPI = (date) => {
        return new Date(date).toISOString().split('T')[0];
    };

    const handleSubmit = async (event) => {
        event.preventDefault();
        const params = {
            ...searchParams,
            departureDate: formatDateForAPI(searchParams.departureDate),
            returnDate: searchParams.returnDate ? formatDateForAPI(searchParams.returnDate) : '',
            nonStop: searchParams.nonStop ? 'true' : 'false',
        };

        const queryString = new URLSearchParams(params).toString();
        const url = `/api/flightoffers/get?${queryString}`;

        try {
            const response = await fetch(url, {
                headers: { 'Accept': 'application/json' }
            });

            if (!response.ok) {           
                throw new Error('Network response was not ok. ', response.statusText);
            }

            const data = await response.json();
            setFlightOffers(data.flights || []);
        } catch (error) {
            console.error('Failed to fetch data:', error);
        }
    };

    const contents = flights.length === 0
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started.</em></p>
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
            <p>This component demonstrates fetching data from the server.</p>
            <form onSubmit={handleSubmit}>
                <input type="text" name="originLocationCode" value={searchParams.originLocationCode} onChange={handleInputChange} placeholder="Origin" />
                <input type="text" name="destinationLocationCode" value={searchParams.destinationLocationCode} onChange={handleInputChange} placeholder="Destination" />
                <input type="date" name="departureDate" value={searchParams.departureDate} onChange={handleInputChange} />
                <input type="date" name="returnDate" value={searchParams.returnDate} onChange={handleInputChange} placeholder="Return Date (Optional)" />
                <input type="number" name="adults" value={searchParams.adults} onChange={handleInputChange} min="1" />
                <label>
                    <input type="checkbox" name="nonStop" checked={searchParams.nonStop} onChange={handleInputChange} /> Non-stop
                </label>
                <input type="text" name="currencyCode" value={searchParams.currencyCode} onChange={handleInputChange} placeholder="Currency Code" />
                <input type="number" name="maxNumberOfResults" value={searchParams.maxNumberOfResults} onChange={handleInputChange} min="1" />
                <button type="submit">Search Flights</button>
            </form>
            {contents}
        </div>
    );
}

export default App;

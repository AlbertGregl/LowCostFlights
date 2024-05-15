import { useState } from 'react';
import FlightSearchForm from './components/FlightSearchForm';
import FlightTable from './components/FlightsTable';
import { fetchFlightOffers } from './services/apiService';
import { formatDateForAPI } from './utils/dateUtils';
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

    const handleSubmit = async (event) => {
        event.preventDefault();
        const params = {
            ...searchParams,
            departureDate: formatDateForAPI(searchParams.departureDate),
            returnDate: searchParams.returnDate ? formatDateForAPI(searchParams.returnDate) : '',
            nonStop: searchParams.nonStop ? 'true' : 'false',
        };

        try {
            const data = await fetchFlightOffers(params);
            setFlightOffers(data.flights || []);
        } catch (error) {
            console.error('Failed to fetch data:', error);
        }
    };

    return (
        <div>
            <h1 id="tabelLabel">Flight Offers</h1>
            <FlightSearchForm
                searchParams={searchParams}
                setSearchParams={setSearchParams}
                handleSubmit={handleSubmit}
            />
            <FlightTable flights={flights} />
        </div>
    );
}

export default App;

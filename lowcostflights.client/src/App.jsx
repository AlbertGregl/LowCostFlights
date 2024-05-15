import { useState } from 'react';
import FlightSearchForm from './components/FlightSearchForm';
import FlightTable from './components/FlightsTable';
import { fetchFlightOffers } from './services/apiService';
import { formatDateForAPI } from './utils/dateUtils';
import { loadFromStorage, saveToStorage } from './utils/storageUtils';
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

        const queryString = new URLSearchParams(params).toString();
        const cachedData = loadFromStorage(queryString);

        if (cachedData) {
            console.log('Using cached data:', cachedData);
            setFlightOffers(cachedData.flights || []);
            return;
        }

        try {
            const data = await fetchFlightOffers(params);
            saveToStorage(queryString, data);
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

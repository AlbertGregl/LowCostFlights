import { useState } from 'react';
import FlightSearchForm from './components/FlightSearchForm';
import FlightTable from './components/FlightsTable';
import { fetchFlightOffers } from './services/apiService';
import { formatDateForAPI } from './utils/dateUtils';
import { loadFromStorage, saveToStorage } from './utils/storageUtils';
import Currencies from './constants/currencies';
import './App.css';

function App() {
    const [flights, setFlightOffers] = useState([]);
    const [loading, setLoading] = useState(false);
    const [searchParams, setSearchParams] = useState({
        originLocationCode: '',
        destinationLocationCode: '',
        departureDate: new Date().toISOString().split('T')[0],
        returnDate: '',
        adults: 1,
        nonStop: false,
        currencyCode: Currencies.EUR,
        maxNumberOfResults: 1
    });

    const handleSubmit = async (event) => {
        event.preventDefault();
        setLoading(true);
        const params = {
            ...searchParams,
            departureDate: formatDateForAPI(searchParams.departureDate),
            returnDate: searchParams.returnDate ? formatDateForAPI(searchParams.returnDate) : '',
            nonStop: searchParams.nonStop ? 'true' : 'false',
        };
        console.log("Submitting with params:", params);

        const queryString = new URLSearchParams(params).toString();
        const cachedData = loadFromStorage(queryString);

        if (cachedData) {
            setFlightOffers(cachedData.flights || []);
            setLoading(false);
            return;
        }

        try {
            const data = await fetchFlightOffers(params);
            saveToStorage(queryString, data);
            setFlightOffers(data.flights || []);
        } catch (error) {
            console.error('Failed to fetch data:', error);
        }
        setLoading(false);
    };

    return (
        <div>
            <h1 id="tabelLabel">Flight Offers</h1>
            <FlightSearchForm
                searchParams={searchParams}
                setSearchParams={setSearchParams}
                handleSubmit={handleSubmit}
            />
            {loading ? <div className="loader"></div> : <FlightTable flights={flights} />}
        </div>
    );
}

export default App;

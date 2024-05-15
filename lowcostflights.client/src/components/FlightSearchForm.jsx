import { useState } from 'react';
import AsyncSelect from 'react-select/async';
import PropTypes from 'prop-types';
import styles from './FlightSearchForm.module.css';
import Currencies from '../constants/currencies';
import { fetchAirportOptions } from '../services/apiService';

const FlightSearchForm = ({ searchParams, setSearchParams, handleSubmit }) => {

    const [originOptions, setOriginOptions] = useState([]);
    const [destinationOptions, setDestinationOptions] = useState([]);

    const loadOptions = async (inputValue, fieldName) => {
        if (inputValue.length < 2) return [];
        try {
            const airportsData = await fetchAirportOptions(inputValue, fieldName);
            const options = airportsData.map(airport => ({
                value: airport.iataCode,
                label: `(${airport.iataCode}) - ${airport.name}`
            }));
            if (fieldName === 'originLocationCode') {
                setOriginOptions(options);
            } else {
                setDestinationOptions(options);
            }
            return options;
        } catch (error) {
            console.error('Error fetching airport data:', error);
            inputValue = inputValue.toUpperCase();
            handleAutoInputChange(inputValue, fieldName);
        }
    };

    const handleAutoInputChange = (newValue, fieldName) => {
        let valueToUpdate = '';

        if (typeof newValue === 'object' && newValue !== null) {
            valueToUpdate = newValue.value || '';
        } else {
            valueToUpdate = newValue || '';
        }

        setSearchParams(prev => ({
            ...prev,
            [fieldName]: valueToUpdate
        }));
    };

    const handleInputChange = (event) => {
        const { name, value, type, checked } = event.target;
        setSearchParams(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };


    return (
        <form onSubmit={handleSubmit}>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Origin [IATA] code</label>
                <AsyncSelect
                    id="originLocationCode"
                    name="originLocationCode"
                    cacheOptions
                    defaultOptions
                    loadOptions={(inputValue) => loadOptions(inputValue, 'originLocationCode')}
                    onChange={(newValue) => handleAutoInputChange(newValue, 'originLocationCode')}
                    value={{
                        value: searchParams.originLocationCode,
                        label: searchParams.originLocationCode ? `${searchParams.originLocationCode}` : ''
                    }}
                    options={originOptions}
                    placeholder="Select or type origin"
                    required
                />
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Destination [IATA] code</label>
                <AsyncSelect
                    id="destinationLocationCode"
                    name="destinationLocationCode"
                    cacheOptions
                    defaultOptions
                    loadOptions={(inputValue) => loadOptions(inputValue, 'destinationLocationCode')}
                    onChange={(newValue) => handleAutoInputChange(newValue, 'destinationLocationCode')}
                    value={{
                        value: searchParams.destinationLocationCode,
                        label: searchParams.destinationLocationCode ? `${searchParams.destinationLocationCode}` : ''
                    }}
                    options={destinationOptions}
                    placeholder="Select or type destination"
                    required
                />
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Departure Date</label>
                <input
                    className={styles.formInput}
                    id="departureDate"
                    type="date"
                    name="departureDate"
                    value={searchParams.departureDate}
                    onChange={handleInputChange}
                    required
                />
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Return Date</label>
                <input
                    className={styles.formInput}
                    id="returnDate"
                    type="date"
                    name="returnDate"
                    value={searchParams.returnDate}
                    onChange={handleInputChange}
                    placeholder="Return Date (Optional)"
                />
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Passengers</label>
                <input
                    className={styles.formInput}
                    id="adults"
                    type="number"
                    name="adults"
                    value={searchParams.adults}
                    onChange={handleInputChange}
                    required
                    min="1"
                />
            </div>
            <div className={styles.formRow}>
                <div className={styles.fullWidth}>
                    <label>
                        <input
                            className={styles.formCheckbox}
                            id="nonStop"
                            type="checkbox"
                            name="nonStop"
                            checked={searchParams.nonStop}
                            onChange={handleInputChange}
                        /> Non-stop
                    </label>
                </div>
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Currency</label>
                <select
                    className={styles.formInput}
                    id="currencyCode"
                    name="currencyCode"
                    value={searchParams.currencyCode}
                    onChange={handleInputChange}
                    required
                >
                    <option value={Currencies.USD}>{Currencies.USD}</option>
                    <option value={Currencies.EUR}>{Currencies.EUR}</option>
                    <option value={Currencies.HRK}>{Currencies.HRK}</option>
                </select>
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Max Number of Results</label>
                <input
                    className={styles.formInput}
                    id="maxNumberOfResults"
                    type="number"
                    name="maxNumberOfResults"
                    value={searchParams.maxNumberOfResults}
                    onChange={handleInputChange}
                    required
                    min="1"
                    max="250"
                />

            </div>

            <button className={styles.submitButton} type="submit">Search Flights</button>
        </form>

    );
};

FlightSearchForm.propTypes = {
    searchParams: PropTypes.object.isRequired,
    setSearchParams: PropTypes.func.isRequired,
    handleSubmit: PropTypes.func.isRequired
};

export default FlightSearchForm;

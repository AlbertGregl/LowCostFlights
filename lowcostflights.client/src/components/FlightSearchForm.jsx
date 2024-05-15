import PropTypes from 'prop-types';
import styles from './FlightSearchForm.module.css';
import Currencies from '../constants/currencies';

const FlightSearchForm = ({ searchParams, setSearchParams, handleSubmit }) => {
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
                <label className={styles.formLabel}>Origin</label>
                <input
                    className={styles.formInput}
                    id="originLocationCode"
                    type="text"
                    name="originLocationCode"
                    value={searchParams.originLocationCode}
                    onChange={handleInputChange}
                    placeholder="Origin"
                    required
                />
            </div>
            <div className={styles.formRow}>
                <label className={styles.formLabel}>Destination</label>
                <input
                    className={styles.formInput}
                    id="destinationLocationCode"
                    type="text"
                    name="destinationLocationCode"
                    value={searchParams.destinationLocationCode}
                    onChange={handleInputChange}
                    placeholder="Destination"
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
                <label className={styles.formLabel}>Currency Code</label>
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

import { loadFromStorage, saveToStorage } from '../utils/storageUtils';

const flightsUrl = '/api/flightoffers/get';
export async function fetchFlightOffers(params) {
    const queryString = new URLSearchParams(params).toString();
    const url = `${flightsUrl}?${queryString}`;

    try {
        const response = await fetch(url, {
            headers: { 'Accept': 'application/json' }
        });

        if (!response.ok) {
            throw new Error(`Network response was not ok: ${response.statusText}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Failed to fetch flight offers:', error);
        throw error;
    }
}


const airportsUrl = '/api/airports/get';
export async function fetchAirportOptions(keyword) {
    const queryString = new URLSearchParams({
        keyword: keyword,
    }).toString();
    const url = `${airportsUrl}?${queryString}`;

    const cachedData = loadFromStorage(queryString);
    if (cachedData) {
        if (cachedData === 'error') {
            throw new Error('Not correct key for the API');
        }
        return cachedData;
    } 

    try {
        const response = await fetch(url, {
            headers: { 'Accept': 'application/json' }
        });

        if (!response.ok) {
            throw new Error(`Network response was not ok: ${response.statusText}`);
        }
        const data = await response.json();
        saveToStorage(queryString, data);

        return data;
    } catch (error) {
        console.error('Failed to fetch airport data:', error);
        saveToStorage(queryString, "error");
        throw error;
    }
}


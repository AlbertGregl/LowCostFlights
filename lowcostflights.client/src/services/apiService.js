const baseUrl = '/api/flightoffers/get';

export async function fetchFlightOffers(params) {
    const queryString = new URLSearchParams(params).toString();
    const url = `${baseUrl}?${queryString}`;

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

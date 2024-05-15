export const loadFromStorage = (key) => {
    try {
        const item = localStorage.getItem(key);
        return item ? JSON.parse(item) : null;
    } catch (error) {
        console.error('Error reading from LocalStorage', error);
        return null;
    }
};

export const saveToStorage = (key, data) => {
    try {
        const item = JSON.stringify(data);
        localStorage.setItem(key, item);
    } catch (error) {
        console.error('Error saving to LocalStorage', error);
    }
};

export const clearStorage = () => {
    localStorage.clear();
}
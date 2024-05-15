export const formatDateForAPI = (date) => {
    return new Date(date).toISOString().split('T')[0];
};

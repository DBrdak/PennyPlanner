const formatDate = (dateString: Date) => {
    const date = new Date(dateString)
    const options: Intl.DateTimeFormatOptions = {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
    };

    return date.toLocaleString('pl-PL', options);
};

export default formatDate
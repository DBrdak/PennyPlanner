const formatNumber = (value: number) => {
    return value.toLocaleString('en', {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2
    });
};

export default formatNumber
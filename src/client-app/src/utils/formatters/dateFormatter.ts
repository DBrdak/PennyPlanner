import dayjs from "dayjs";

const formatDate = (dateString: Date) => {
    return dayjs(dateString).format("DD.MM.YYYY HH:mm");
};

export default formatDate
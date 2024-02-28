import {Money} from "../../models/shared/money";
import formatNumber from "./numberFormatter";

const formatMoney = (value: Money) => {
    return `${formatNumber(value.amount)} ${value.currency}`
};

export default formatMoney
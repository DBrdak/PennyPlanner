import {DateTimeRange} from "../../models/shared/dateTimeRange";

export function isDateTimeRangeContainsDate(dateRange: DateTimeRange, date: Date) {
    return dateRange.start <= date && dateRange.end >= date
}
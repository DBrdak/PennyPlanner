import {DateTimeRange} from "../../models/shared/dateTimeRange";

export function isDateTimeRangeContainsDate(dateRange: DateTimeRange, date: Date) {
    return new Date(dateRange.start) <= date && new Date(dateRange.end) >= date
}
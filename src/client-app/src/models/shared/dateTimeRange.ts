export interface DateTimeRange {
    start: Date
    end: Date
}

export class DateTimeRange implements DateTimeRange {
    constructor(start: Date, end: Date) {
        this.start = start
        this.end = end
    }

    isContainDate(date: Date) {
        return this.start <= date && this.end >= date
    }
}
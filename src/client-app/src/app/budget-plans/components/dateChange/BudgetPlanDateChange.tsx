import {Paper, styled, Typography, useMediaQuery} from "@mui/material";
import theme from "../../../theme";
import DateChangeButton from "./DateChangeButton";
import {
    KeyboardArrowLeft,
    KeyboardArrowRight,
    KeyboardDoubleArrowLeft,
    KeyboardDoubleArrowRight
} from "@mui/icons-material";
import dayjs from "dayjs";
import {useState} from "react";

interface BudgetPlanDateChangeProps{
    date: Date,
    setDate: (value: (((prevState: Date) => Date) | Date)) => void
    prevDateAccessible: boolean
}

const DateTypography = styled(Typography)({
    transition: 'opacity 0.3s ease-in-out, transform 0.3s ease-in'
});

export function BudgetPlanDateChange({ date, setDate, prevDateAccessible }: BudgetPlanDateChangeProps) {
    const isMobile = useMediaQuery(theme.breakpoints.down('md'))
    const [opacity, setOpacity] = useState(1)
    const [transform, setTransform] = useState(0)

    const isDateIntervalGreaterThanYear = (start: Date, end: Date) => end.getTime() - start.getTime() > 365 * 24 * 60 * 60 * 1000
    console.log(date.getTime() - new Date().getTime())

    const handleDateChange = (months: number) => {
        setOpacity(0)
        setTransform(months > 0 ? 50 : -50)
        setTimeout(() => {
            let newDate = dayjs(date).add(months, "month").toDate()

            if(isDateIntervalGreaterThanYear(new Date(), newDate)) {
                newDate = dayjs(new Date()).add(1, "year").toDate()
            }

            setDate(newDate)
            setOpacity(1)
            setTransform(0)
        }, 300)
    }

    return (
        <Paper sx={{
            width: '100%',
            display:'flex',
            justifyContent: 'space-between',
            alignItems: 'center',
            height: '100%',
            borderRadius: '20px',
            padding: theme.spacing(isMobile ? 1 : 4),
        }}>
            {!isMobile &&
                <DateChangeButton disabled={!prevDateAccessible} disableRipple left onClick={() => handleDateChange(-12)}>
                    <KeyboardDoubleArrowLeft sx={{fontSize: theme.spacing(15)}} />
                </DateChangeButton>
            }
            <DateChangeButton disabled={!prevDateAccessible} disableRipple left onClick={() => handleDateChange(-1)}>
                <KeyboardArrowLeft sx={{fontSize: theme.spacing(isMobile ? 8 : 15)}} />
            </DateChangeButton>
            <DateTypography variant={ isMobile ? 'h4' : 'h3'} textAlign={'center'} sx={{opacity, transform: `translateX(${transform}px)`}}>
                {dayjs(date).format("MMMM YYYY")}
            </DateTypography>
            <DateChangeButton disabled={!isDateIntervalGreaterThanYear(new Date(), date)} disableRipple right onClick={() => handleDateChange(1)}>
                <KeyboardArrowRight sx={{fontSize: theme.spacing(isMobile ? 8 : 15)}} />
            </DateChangeButton>
            {!isMobile &&
                <DateChangeButton disabled={!isDateIntervalGreaterThanYear(new Date(), date)} disableRipple right onClick={() => handleDateChange(12)}>
                    <KeyboardDoubleArrowRight sx={{fontSize: theme.spacing(15)}} />
                </DateChangeButton>
            }
        </Paper>
    );
}
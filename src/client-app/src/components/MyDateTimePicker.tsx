import React from 'react';
import { LocalizationProvider, DateTimePicker, MobileDateTimePicker } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import dayjs from 'dayjs';
import {SxProps, Theme} from "@mui/material";

interface MyDateTimePickerProps {
    name: string
    label: string
    isMobile: boolean;
    values: { transactionDateTime: Date };
    setValues: (values: any) => void;
    maxDateTime?: Date
    sx: SxProps<Theme>
}

const MyDateTimePicker: React.FC<MyDateTimePickerProps> = ({ isMobile, values, setValues, maxDateTime, sx, name, label }) => {
    const handleChange = (value: dayjs.Dayjs | null) => {
        setValues({
            ...values,
            transactionDateTime: value ? value.toDate() : new Date()
        });
    };

    return (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
            {isMobile ? (
                <MobileDateTimePicker
                    maxDateTime={dayjs(maxDateTime)}
                    name={name}
                    label={label}
                    value={dayjs(values.transactionDateTime)}
                    onChange={handleChange}
                    sx={sx}
                />
            ) : (
                <DateTimePicker
                    maxDateTime={dayjs(maxDateTime)}
                    name={name}
                    label={label}
                    value={dayjs(values.transactionDateTime)}
                    onChange={handleChange}
                    sx={sx}
                />
            )}
        </LocalizationProvider>
    );
};

export default MyDateTimePicker;
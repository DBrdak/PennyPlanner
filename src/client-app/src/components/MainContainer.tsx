import React, { ReactNode } from 'react';
import { Box, Paper } from '@mui/material';
import theme from "../app/theme";

interface MainContainer {
    backgroundColor?: string;
    children: ReactNode;
}

const MainContainer: React.FC<MainContainer> = ({ backgroundColor = '#fff', children }) => {

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                minHeight: '100vh',
                backgroundColor: theme.palette.background.default,
            }}
        >
            {children}
        </Box>
    );
};

export default MainContainer;
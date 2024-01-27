import React, { ReactNode } from 'react';
import { Box } from '@mui/material';
import theme from "../app/theme";

interface MainContainerProps {
    children: ReactNode;
}

const MainContainer: React.FC<MainContainerProps> = ({ children }) => {

    return (
        <Box
            sx={{
                display: 'flex',
                justifyContent: 'center',
                alignItems: 'center',
                minHeight: '100svh',
                backgroundColor: theme.palette.background.default,
            }}
        >
            {children}
        </Box>
    )
}

export default MainContainer
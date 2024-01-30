import React, {CSSProperties, ReactNode} from 'react';
import {Stack} from '@mui/material';

interface CenteredStack {
    direction?: 'row' | 'column'
    style?: CSSProperties
    children: ReactNode
}

const CenteredStack: React.FC<CenteredStack> = ({ direction = 'column', style, children }) => {
    return (
        <Stack direction={direction} style={{justifyContent: 'center', alignItems: 'center', ...style}}>
            {children}
        </Stack>
    );
};

export default CenteredStack;
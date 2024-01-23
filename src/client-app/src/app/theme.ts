import { createTheme } from '@mui/material/styles';
import { red } from '@mui/material/colors';

const theme = createTheme({
    palette: {
        mode: 'dark', // Use dark mode
        primary: {
            main: '#5b1ae7',
            light: '#8143f8'
        },
        secondary: {
            main: '#b015d3',
        },
        error: {
            main: '#ff1744',
        },
        info: {
            main: '#ff9800',
        },
        background: {
            default: '#0d0418',
            paper: '#1a1a1a'
        },
        text: {
            primary: '#ffffff',
            secondary: '#bdbdbd',
        },
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: {
                '*::-webkit-scrollbar': {
                    width: '10px',
                },
                '*::-webkit-scrollbar-track': {
                    background: '#f5f5f5',
                },
                '*::-webkit-scrollbar-thumb': {
                    backgroundColor: '#888',
                    borderRadius: '15px',
                    '&:hover': {
                        backgroundColor: '#555',
                    },
                },
            },
        },
        MuiButton: {
            styleOverrides: {
                root: {
                    borderRadius: 50
                },
            },
        },
    },
});

export default theme;
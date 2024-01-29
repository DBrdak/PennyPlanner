import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    palette: {
        mode: 'dark',
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
            secondary: '#bdbdbd'
        },
    },
    breakpoints: {
        values: {
            xs: 0,
            sm: 600,
            md: 900,
            lg: 1200,
            xl: 1536,
        }
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
        MuiListItemButton:{
            styleOverrides: {
                root:{
                    "&.Mui-disabled": {
                        opacity: 1,
                    }
                }
            }
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
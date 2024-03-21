import React from 'react';
import ReactDOM from 'react-dom/client';
import './styles/index.css';
import {CssBaseline, ThemeProvider} from "@mui/material";
import theme from "./app/theme";
import {RouterProvider} from "react-router-dom";
import {router} from "./router/Routes";


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);
root.render(
    <ThemeProvider theme={theme}>
        <CssBaseline />
        <RouterProvider router={router} />
    </ThemeProvider>
);

import React from 'react';
import {Outlet, ScrollRestoration} from "react-router-dom";
import ModalContainer from '../components/ModalContainer';
import {ToastContainer} from "react-toastify";
import {FadeContainer} from "../components/FadeContainer";

function App() {

    return(
    <FadeContainer>
            <ScrollRestoration />
            <ModalContainer />
            <ToastContainer position={'bottom-right'} theme={'colored'} />
            <Outlet />
    </FadeContainer>
    )
}

export default App;

import React, {useEffect} from 'react';
import {Outlet, ScrollRestoration, useNavigate} from "react-router-dom";
import ModalContainer from '../components/ModalContainer';
import {ToastContainer} from "react-toastify";
import MainContainer from "../components/MainContainer";

function App() {

    return(
    <>
        <ScrollRestoration />
        <ModalContainer />
        <ToastContainer position={'bottom-right'} theme={'colored'} />
        <MainContainer>
            <Outlet />
        </MainContainer>
    </>
    )
}

export default App;

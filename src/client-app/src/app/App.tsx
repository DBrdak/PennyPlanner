import React from 'react';
import {Outlet, ScrollRestoration} from "react-router-dom";
import ModalContainer from '../components/ModalContainer';
import {ToastContainer, Bounce} from "react-toastify";
import {FadeContainer} from "../components/FadeContainer";
import 'react-toastify/dist/ReactToastify.css';


function App() {

    return(
    <FadeContainer>
            <ScrollRestoration />
            <ModalContainer />
            <Outlet />
            <ToastContainer
                position="bottom-right"
                autoClose={3500}
                limit={5}
                hideProgressBar={false}
                newestOnTop
                closeOnClick
                rtl={false}
                pauseOnFocusLoss={false}
                draggable
                pauseOnHover={false}
                theme="colored"
                transition={Bounce}
            />
    </FadeContainer>
    )
}

export default App;

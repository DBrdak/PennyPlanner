import React from 'react';
import {Outlet, ScrollRestoration, useLocation} from "react-router-dom";
import ModalContainer from '../components/ModalContainer';
import {ToastContainer, Bounce} from "react-toastify";
import {FadeContainer} from "../components/FadeContainer";
import 'react-toastify/dist/ReactToastify.css';
import WelcomePage from "./welcome/WelcomePage";


function App() {
    const location = useLocation()

    return(
    <FadeContainer>
        <ScrollRestoration />
        <ModalContainer />
        {
            location.pathname === '/' ?
                <WelcomePage />
                :
                <Outlet />
        }
        <ToastContainer
            position="bottom-right"
            autoClose={3500}
            limit={1}
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

import React from 'react';
import {Outlet, ScrollRestoration} from "react-router-dom";
import ModalContainer from '../components/ModalContainer';
import {ToastContainer} from "react-toastify";

function App() {
  return(
      <>
        <ScrollRestoration />
        <ModalContainer />
        <ToastContainer position={'bottom-right'} theme={'colored'} />
        <Outlet />
      </>
  )
}

export default App;

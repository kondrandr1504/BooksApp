import React from 'react';
import Appcss from './App.module.css';
import BasicTabs from './Components/BasicTabs';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function App() {
  return (
    <div className={Appcss.wrapper}>
      <BasicTabs />
      <ToastContainer position="top-center" />
    </div>


  );
}

export default App;

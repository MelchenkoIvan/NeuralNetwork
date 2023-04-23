import 'semantic-ui-css/semantic.min.css';
import 'react-toastify/dist/ReactToastify.css';

import './App.css';
import LoginForm from './forms/LoginForm';
import { ToastContainer } from 'react-toastify';
import { useAppDispatch, useAppSelector} from './app/hooks'
import { Container } from 'semantic-ui-react';
import SymptomesForm from './forms/SymptomesForm';
import { useEffect } from 'react';
import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";

const App = () => {  
  return (
    <div className="App">
      <ToastContainer position="bottom-right" hideProgressBar />
      <Router>
        <Routes>
          <Route path={"/"} element={<SymptomesForm />} />
          <Route path={"/login"} element={<LoginForm />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;

import 'semantic-ui-css/semantic.min.css';
import 'react-toastify/dist/ReactToastify.css';

import './App.css';
import LoginForm from './forms/LoginForm';
import { ToastContainer } from 'react-toastify';
import {
  BrowserRouter as Router,
  Routes,
  Route
} from "react-router-dom";
import NeuralNetworkPage from './NeuralNetworkPage';

const App = () => {  
  return (
    <div className="App">
      <ToastContainer position="bottom-right" hideProgressBar />
      <Router>
        <Routes>
          <Route path={"/"} element={<NeuralNetworkPage />} />
          <Route path={"/login"} element={<LoginForm />} />
        </Routes>
      </Router>
    </div>
  );
}

export default App;

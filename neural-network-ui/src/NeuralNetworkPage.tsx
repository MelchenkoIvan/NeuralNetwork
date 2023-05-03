import React, {FC, useEffect} from "react";
import { useAppSelector} from './app/hooks'
import { useNavigate } from "react-router-dom";
import SymptomesForm from "./forms/SymptomesForm";
import ChangeNeuralNetworkType from "./ChangeNeuralNetworkType";

const NeuralNetworkPage:FC = () => {
    const authdata = useAppSelector((state) => state.user.authdata);

    const navigate = useNavigate();
  
    useEffect(() => {
      if(authdata.length == 0) 
        navigate("/login")
    }, [authdata]);

    return(
        <>
            <ChangeNeuralNetworkType />
            <SymptomesForm />
        </>
    )
}
export default NeuralNetworkPage;
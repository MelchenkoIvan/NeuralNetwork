import React, {FC, useEffect} from "react";
import { useAppSelector} from './app/hooks'
import { useNavigate } from "react-router-dom";
import SymptomesForm from "./forms/SymptomesForm";
import ChangeNeuralNetworkType from "./ChangeNeuralNetworkType";

const NeuralNetworkPage:FC = () => {
    const currentUser = useAppSelector((state) => state.user.userName);

    const navigate = useNavigate();
  
    useEffect(() => {
      if(currentUser.length == 0) 
        navigate("/login")
    }, [currentUser]);

    return(
        <>
            <ChangeNeuralNetworkType />
            <SymptomesForm />
        </>
    )
}
export default NeuralNetworkPage;
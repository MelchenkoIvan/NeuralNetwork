import './App.css';
import { FC } from "react";
import { Button } from 'semantic-ui-react';
import { useAppDispatch, useAppSelector} from './app/hooks';
import { changeNeuralNetworkType, NNTypes } from './features/neuralNetworkSlice';

const ChangeNeuralNetworkType:FC = () => {
    const dispatch = useAppDispatch();
    const selectedNN = useAppSelector((state) => state.selectedNNType.nnType);

    return(
        <div className='changeNeuralNetworkType'>
            <Button.Group>
                <Button className={selectedNN === NNTypes.FFNN ? "changeNeuralNetworkTypeButton" : ""}  onClick={() => {dispatch(changeNeuralNetworkType(NNTypes.FFNN))}} type="button">FFNN</Button>
                <Button.Or />
                <Button className={selectedNN === NNTypes.RNN ? "changeNeuralNetworkTypeButton" : ""} onClick={() => {dispatch(changeNeuralNetworkType(NNTypes.RNN))}} type="button">RNN</Button>
            </Button.Group>
        </div>
    )
}
export default ChangeNeuralNetworkType;
import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface Symptomes{
    Age: string,
    Sex: string,
    Trestbps: string,
    Chol: string,
    Fbs: string,
    Restecg: string,
    Thalach: string,
    Exang: string,
    Oldpeak: string,
    Slope: string,
    Ca: string,
    Thal: string
};

export enum NNTypes{
    RNN = 1,
    FFNN = 2
}

export interface neuralNetworkState{
    nnType: NNTypes;
    result: number|null;
};

const initialState: neuralNetworkState = {
    nnType: NNTypes.FFNN,
    result: null
};

const neuralNetworkSlice = createSlice({
    name: 'neuralNetwork',
    initialState,
    reducers: {
        changeNeuralNetworkType(state, action: PayloadAction<NNTypes>){
            state.nnType = action.payload;
        },
        setResult(state, action: PayloadAction<number|null>){
            state.result = action.payload;
        }
    }
})

export const { changeNeuralNetworkType, setResult } = neuralNetworkSlice.actions;
export default neuralNetworkSlice.reducer;

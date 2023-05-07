import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface Symptomes{
    Age: string,
    Sex: string,
    Cp: string,
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

export interface Result{
    symptomId:number,
    age: string,
    sex: string,
    cp: string,
    trestbps: string,
    chol: string,
    fbs: string,
    restecg: string,
    thalach: string,
    exang: string,
    oldpeak: string,
    slope: string,
    ca: string,
    thal: string,
    result: number,
    nnType: NNTypes,
};

export enum NNTypes{
    RNN = 1,
    FFNN = 2
}

export interface neuralNetworkState{
    nnType: NNTypes;
    results: Result[]
};

const initialState: neuralNetworkState = {
    nnType: NNTypes.FFNN,
    results: []
};

const neuralNetworkSlice = createSlice({
    name: 'neuralNetwork',
    initialState,
    reducers: {
        changeNeuralNetworkType(state, action: PayloadAction<NNTypes>){
            state.nnType = action.payload;
        },
        setResult(state, action: PayloadAction<Result[]>){
            state.results = action.payload;
        }
    }
})

export const { changeNeuralNetworkType, setResult } = neuralNetworkSlice.actions;
export default neuralNetworkSlice.reducer;

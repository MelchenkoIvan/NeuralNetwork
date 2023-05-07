
import { NNTypes, Result, Symptomes } from '../features/neuralNetworkSlice';
import * as conf from './defaultConfig';
import notificationService from './notificationService';

export const neuralNetworkService = {
    async Predict(symptomes: Symptomes, neuralNetworkType: NNTypes) {
        try {
            let suffix = neuralNetworkType == NNTypes.FFNN ? 'NeuralNetwork/feedforward' : 'NeuralNetwork/recurrent';
            let response = await conf.api.post<number>(suffix, symptomes)
                .then(response => response);

            if(response.status == 200)
                notificationService.PredictionRequestSuccess()
        }
        catch (e) {
            console.log(e);
        }
    },
    async List() {
        try {
            let data = await conf.api.post<Result[]>("NeuralNetwork/list")
                .then(response => response.data);

            return data;
        }
        catch (e) {
            console.log(e);
        }
    },

};
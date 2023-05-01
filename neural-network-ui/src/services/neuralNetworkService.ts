import { NNTypes, Symptomes } from '../features/neuralNetworkSlice';
import * as conf from './defaultConfig';

export const neuralNetworkService = {
    async Predict(symptomes: Symptomes, neuralNetworkType: NNTypes) {
        try {
            let suffix = neuralNetworkType == NNTypes.FFNN ? 'neuralNetowrk/feedforward' : 'neuralNetwork/recurrent';
            let data = await conf.api.post<number>(suffix, symptomes)
                .then(response => response.data);
            return data;
        }
        catch (e) {
            console.log(e);
        }
    }
};
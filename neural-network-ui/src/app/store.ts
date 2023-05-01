import { configureStore } from '@reduxjs/toolkit';
import userReducer from '../features/userSlice';
import nnTypeReducer from '../features/neuralNetworkSlice';

export const store = configureStore({
    reducer:{
        user: userReducer,
        selectedNNType: nnTypeReducer
    }
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
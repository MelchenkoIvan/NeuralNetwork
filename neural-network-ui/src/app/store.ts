import { configureStore } from '@reduxjs/toolkit';
import userReducer from '../features/userSlice';
//import { reducer as formReducer } from 'redux-form';

export const store = configureStore({
    reducer:{
        user: userReducer,
//        form: formReducer
    }
});

export type AppDispatch = typeof store.dispatch;
export type RootState = ReturnType<typeof store.getState>;
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { store } from '../app/store';

export interface UserLogin{
    userName: string;
    password: string;
};

export interface UserState{
    authdata: string;
};

const initialState: UserState = {
    authdata: ""
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        logout(state){
            state.authdata = "";
        },
        login(state, action: PayloadAction<string>){
            state.authdata = action.payload;
        }
    }
})

export const { logout, login } = userSlice.actions;
export default userSlice.reducer;

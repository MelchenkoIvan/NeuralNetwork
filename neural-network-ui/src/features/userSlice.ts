import { createSlice, PayloadAction } from '@reduxjs/toolkit';

export interface UserLogin{
    userName: string;
    password: string;
};

export interface UserState{
    userName: string;
};

const initialState: UserState = {
    userName: ""
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        logout(state){
            state.userName = "";
        },
        login(state, action: PayloadAction<string>){
            state.userName = action.payload;
        }
    }
})

export const { logout, login } = userSlice.actions;
export default userSlice.reducer;

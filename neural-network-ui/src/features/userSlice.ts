import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { createApi, fetchBaseQuery} from '@reduxjs/toolkit/query/react';

interface User{
    id: number;
    userName: string;
};

interface UserState{
    userName: string;
};

const initialState: UserState = {
    userName: ""
};

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        logout(state, action: PayloadAction<string>){
            state.userName = action.payload;
        },
        login(state, action: PayloadAction<string>){
            state.userName = action.payload;
        }
    }
})

export const { logout, login } = userSlice.actions;
export default userSlice.reducer;
import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { RootState } from "~/redux/store";

export type AuthState = {
	userName: string;
	amount: number;
	role: Array<string>;
};

const initialState: AuthState = {
	userName: "",
	amount: 0,
	role: [],
};

export const authSlice = createSlice({
	name: "auth",
	initialState,
	reducers: {
		setUserName: (state) => {
			state.userName = "dispatch username";
		},
		setAmountWithPayload: (state, action: PayloadAction<number>) => {
			state.amount += action.payload;
		},
	},
});

export const { setUserName, setAmountWithPayload } = authSlice.actions;

export default authSlice.reducer;

import type { NextPage } from "next";
import AxiosClient, { BASE_API_PATH } from "~/utils/ApiUtil";
import { AxiosError, AxiosResponse } from "axios";
import { useAppDispatch, useAppSelector } from "~/redux/hooks";
import { setAmountWithPayload, setUserName } from "~/redux/slices/authSlice";

const Home: NextPage = () => {
	const userState = useAppSelector((state) => state.auth);
	const dispatch = useAppDispatch();
	const onClickTestAxios = async () => {
		const response = (await AxiosClient.get("/test").catch((err: AxiosError) => console.log(err))) as AxiosResponse<any[]>;
		const result = response.data;
		if (result) {
			console.log("result", result);
		}
	};

	const onClickDispatchUserName = () => {
		dispatch(setUserName());
	};
	const onClickDispatchAmount = () => {
		dispatch(setAmountWithPayload(5));
	};

	return (
		<div className="flex flex-col">
			<h1 className="text-3xl font-bold underline">Test style tailwind</h1>
			<button className="w-[200px] bg-amber-500 p-2" onClick={onClickTestAxios}>
				Click
			</button>
			<div>{userState.userName}</div>
			<button className="w-[200px]" onClick={onClickDispatchUserName}>
				Test Dispatch Username
			</button>
			<div>{userState.amount}</div>
			<button className="w-[200px]" onClick={onClickDispatchAmount}>
				Test Dispatch Amount with Payload
			</button>
		</div>
	);
};

export default Home;

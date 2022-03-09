import type { NextPage } from "next";
import AxiosClient, { BASE_API_PATH } from "~/utils/ApiUtil";
import { AxiosError, AxiosResponse } from "axios";
import { useAppDispatch, useAppSelector } from "~/redux/hooks";
import { setAmountWithPayload, setUserName } from "~/redux/slices/authSlice";
import {FaRegEnvelope} from "react-icons/fa";
import {MdLockOutline} from "react-icons/md"
import Head from "next/head";

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
		<div className="flex flex-col items-center justify-center min-h-screen py-2 bg-gray-100">
			<Head>
			<title>Login</title>
			<link rel="icon" href="/favicon.ico"></link>
			</Head>
			<main className="flex item-center justify-center w-full px-10 text-center"> 
				<div className="bg-white rounded-3xl shadow-2xl max-w-4xl">
					<div className="p-5">
						<div className="text-left font-bold">
							<span className="text-green-400"> Đăng ký</span>
						</div>
						<div className="py-10">
							<h2 className="text-1xl font-bold">Sign in your Account</h2>
						</div>
						<div className ="flex justify-between">
							<div className="flex items-center mr-2"> 
								<p >Email :</p>
							</div>
							<div className=" bg-gray-200 w-80 p-2 rounded-2xl flex items-center mb-3">
								<FaRegEnvelope className ="bg-gray-400 mr-2"></FaRegEnvelope>
								<input type="email" name="email" className="bg-gray-200  outlint-none text-sm flex-1" ></input>
							</div>
						</div>
						
						<div className ="flex justify-between">
							<p className="flex items-center mr-2">Password :</p>
							<div className="bg-gray-200 w-80 p-2 rounded-2xl flex items-center mb-3">
								<MdLockOutline className ="bg-gray-400 mr-2"></MdLockOutline>
								<input type="password" name="password" className="bg-gray-200 outlint-none text-sm flex-1" ></input>
							</div>
						</div>
						
						<div className ="flex justify-between">
							<p className="flex items-center mr-2">Họ và tên :</p>
							<div className="bg-gray-200 w-80 p-2 rounded-2xl flex items-center mb-3">
								{/* <MdLockOutline className ="bg-gray-400 mr-2"></MdLockOutline> */}
								<input type="text" name="fullname" className="bg-gray-200 outlint-none text-sm flex-1" ></input>
							</div>
						</div>

						<div className ="flex justify-between">
							<p className="flex items-center mr-2">Số điện thoại :</p>
							<div className="bg-gray-200 w-80 p-2 rounded-2xl flex items-center mb-3">
								{/* <MdLockOutline className ="bg-gray-400 mr-2"></MdLockOutline> */}
								<input type="number" name="phone" className="bg-gray-200 outlint-none text-sm flex-1" ></input>
							</div>
						</div>
						<div className ="flex justify-between">
							<p className="flex items-center mr-2">Mô tả dịch vụ :</p>
							<div className="bg-gray-200 w-80 p-2 rounded-2xl flex items-center mb-3">
								{/* <MdLockOutline className ="bg-gray-400 mr-2"></MdLockOutline> */}
								<textarea name="description" className="bg-gray-200 outlint-none text-sm flex-1" ></textarea>
							</div>
						</div>
						<div className ="flex justify-between">
							<p className="flex items-center mr-2">Thành viên giới thiệu :</p>
							<div className="bg-gray-200 w-80 p-2 rounded-2xl flex items-center mb-3">
								{/* <MdLockOutline className ="bg-gray-400 mr-2"></MdLockOutline> */}
								<input name="employeeCode" className="bg-gray-200 outlint-none text-sm flex-1" ></input>
							</div>
						</div>	
					</div>
				</div>

			</main>

		</div>
		// <div className="flex flex-col">
		// 	<h1 className="text-3xl font-bold underline">Test style tailwind</h1>
		// 	<button className="w-[200px] bg-amber-500 p-2" onClick={onClickTestAxios}>
		// 		Click
		// 	</button>
		// 	<div>{userState.userName}</div>
		// 	<button className="w-[200px]" onClick={onClickDispatchUserName}>
		// 		Test Dispatch Username
		// 	</button>
		// 	<div>{userState.amount}</div>
		// 	<button className="w-[200px]" onClick={onClickDispatchAmount}>
		// 		Test Dispatch Amount with Payload
		// 	</button>
		// </div>
	);
};

export default Home;

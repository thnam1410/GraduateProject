import React from "react";
import { useRouter } from "next/router";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import { ApiResponse } from "~/src/pages/types/api.type";

import { REGISTER_API } from "~/src/constants/apis/auth.api";

interface IFormInput {
	userName: string;
	email: string;
	phoneNumber: string;
	password: string;
	confirmPassword: string;
	fullName: string;
}

export default function Register() {
	const router = useRouter();

	const onCheckbox = () => {
		// v will be true or false
	};
	const onRedirectLogin = () => router.push("/login");
	const onFinishFailed = (errorInfo: any) => {
		console.log("Failed:", errorInfo);
	};

	const dataSource: IFormInput = {
		userName: "Pham Ngoc Danh",
		email: "check ",
		phoneNumber: "0927140859",
		password: "12345",
		confirmPassword: "true",
		fullName: "Pham Ngoc Danh",
	};

	const onFinish = (values: IFormInput) => {
		ApiUtil.Axios.post<ApiResponse>(REGISTER_API, values)
			.then((res) => {
				if (res.data.success) {
					ApiUtil.ToastSuccess("Đăng ký thành công! Vui lòng đăng nhập");
					onRedirectLogin();
				} else {
					ApiUtil.ToastError("Đăng ký thất bại! Vui lòng thử lại");
					console.log(res?.data?.message);
				}
			})
			.catch((err) => {
				console.log(err);
			});
	};
	return (
		<div className="container mx-auto">
			<div className="flex justify-center px-6 my-12">
				<div className="w-full xl:w-3/4 lg:w-11/12 flex">
					<div
						className="w-full h-auto bg-gray-400 hidden lg:block lg:w-5/12 bg-cover rounded-l-lg"
						style={{ backgroundImage: "url('https://source.unsplash.com/Mv9hjnEUHR4/600x800'" }}
					></div>
					<div className="w-full lg:w-7/12 bg-white p-5 rounded-lg lg:rounded-l-none">
						<h3 className="pt-4 text-2xl text-center">Create an Account!</h3>
						<form className="px-8 pt-6 pb-8 mb-4 bg-white rounded">
							<div className="mb-4 md:flex md:justify-between">
								<div className="mb-4 md:mr-2 md:mb-0">
									<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="firstName">
										Tên tài khoản
									</label>
									<input
										className="w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
										id="firstName"
										type="text"
										placeholder="First Name"
									/>
								</div>
								<div className="md:ml-2">
									<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="lastName">
										Địa chỉ Email
									</label>
									<input
										className="w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
										id="lastName"
										type="text"
										placeholder="Last Name"
									/>
								</div>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="email">
									Email
								</label>
								<input
									className="w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
									id="email"
									type="email"
									placeholder="Email"
								/>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="phoneNumber">
									Số điện thoại
								</label>
								<input
									className="w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
									id="phoneNumber"
									type="phoneNumber"
									placeholder="Số điện thoại"
								/>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="decriptions">
									Mô tả dịch vụ
								</label>
								<input
									className="w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
									id="decriptions"
									type="decriptions"
									placeholder="Mô tả dịch vụ"
								/>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="referralMember">
									Thành viên giới thiệu
								</label>
								<input
									className="w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
									id="referralMember"
									type="referralMember"
									placeholder="Thành viên giới thiệu"
								/>
							</div>
							<div className="mb-4 md:flex md:justify-between">
								<div className="mb-4 md:mr-2 md:mb-0">
									<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="password">
										Password
									</label>
									<input
										className="w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border border-red-500 rounded shadow appearance-none focus:outline-none focus:shadow-outline"
										id="password"
										type="password"
										placeholder="******************"
									/>
									<p className="text-xs italic text-red-500">Mật khẩu.</p>
								</div>
								<div className="md:ml-2">
									<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="c_password">
										Xác nhận mật khẩu
									</label>
									<input
										className="w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline"
										id="c_password"
										type="password"
										placeholder="******************"
									/>
								</div>
							</div>
							<div className="mb-6 text-center">
								<button
									className="w-full px-4 py-2 font-bold text-white bg-blue-500 rounded-full hover:bg-blue-700 focus:outline-none focus:shadow-outline"
									type="button"
								>
									Register Account
								</button>
							</div>
							<hr className="mb-6 border-t" />
							<div className="text-center">
								<a className="inline-block text-sm text-blue-500 align-baseline hover:text-blue-800" href="#">
									Forgot Password?
								</a>
							</div>
							<div className="text-center">
								<a className="inline-block text-sm text-blue-500 align-baseline hover:text-blue-800" href="./index.html">
									Already have an account? Login!
								</a>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
		// <Layout title="Đăng ký">
		// 	<Auth title="Đăng ký">
		// 		<form>
		// 			<Input fullWidth>
		// 				<input type="text" placeholder="Tên tài khoản" />
		// 			</Input>
		// 			<Input fullWidth>
		// 				<input type="email" placeholder="Địa chỉ Email" />
		// 			</Input>
		// 			<Input fullWidth>
		// 				<input type="password" placeholder="Mật khẩu" />
		// 			</Input>
		// 			<Input fullWidth>
		// 				<input type="password" placeholder="Xác nhận mật khẩu" />
		// 			</Input>
		// 			<Input fullWidth>
		// 				<input type="number" placeholder="Số điện thoại" />
		// 			</Input>
		// 			<Input fullWidth>
		// 				<input type="text" placeholder="Mô tả dịch vụ" />
		// 			</Input>
		// 			<Input fullWidth>
		// 				<input type="text" placeholder="Thành viên giới thiệu" />
		// 			</Input>
		// 			<Button onClick={() => onFinish(dataSource)} status="Success" type="button" shape="SemiRound" fullWidth>
		// 				Đăng ký
		// 			</Button>
		// 		</form>
		// 		<p>
		// 			Bank đã có tài khoản?{" "}
		// 			<Link href="/auth/login">
		// 				<a>Đăng nhập</a>
		// 			</Link>
		// 		</p>
		// 	</Auth>
		// </Layout>
	);
}

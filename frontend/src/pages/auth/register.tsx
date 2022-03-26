import React from "react";
import { useRouter } from "next/router";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import { ApiResponse } from "~/src/pages/types/api.type";

import { REGISTER_API } from "~/src/constants/apis/auth.api";
import TextArea from "antd/lib/input/TextArea";
import { useForm } from "react-hook-form";
import { ErrorMessage } from "@hookform/error-message";

interface IFormInput {
	userName: string;
	email: string;
	phoneNumber: string;
	password: string;
	confirmPassword: string;
	// decriptions: string;
	// referralMember: string;
}

export default function Register() {
	const router = useRouter();
	const {
		register,
		formState: { errors },
		handleSubmit,
	} = useForm<IFormInput>();

	const onCheckbox = () => {
		// v will be true or false
	};
	const onRedirectLogin = () => router.push("/login");
	const onFinishFailed = (errorInfo: any) => {
		console.log("Failed:", errorInfo);
	};

	const onFinish = handleSubmit((values: IFormInput) => {
		const password = values.password;
		const confirmPassword = values.confirmPassword;
		if (password !== confirmPassword) return ApiUtil.ToastError("Mật khẩu xác nhận không trùng nhau ! Vui lòng kiểm tra lại");
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
	});

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
						<form className="px-8 pt-6 pb-8 mb-4 bg-white rounded" onSubmit={onFinish}>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="userName">
									Tên tài khoản
								</label>
								<input
									className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
										errors?.userName && "border-red-500"
									}`}
									id="userName"
									type="text"
									placeholder="Tên tài khoản"
									{...register("userName", {
										required: {
											value: true,
											message: "Vui lòng nhập tên tài khoản",
										},
										minLength: {
											value: 8,
											message: "Ký tự không đủ",
										},
										maxLength: {
											value: 120,
											message: "Ký tự vượt quá ký tự cho phép",
										},
										pattern: {
											value: /^(?=[a-zA-Z0-9._]{8,20}$)(?!.*[_.]{2})[^_.].*[^_.]$/i,
											message: "Vui lòng kiểm tra lại các ký tự không đúng cho tên tài khoản",
										},
									})}
								/>
								<ErrorMessage
									errors={errors}
									name="userName"
									render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
								/>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="email">
									Email
								</label>
								<input
									className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
										errors?.email && "border-red-500"
									}`}
									id="email"
									type="email"
									placeholder="Email"
									{...register("email", {
										required: {
											value: true,
											message: "Vui lòng nhập email",
										},
										minLength: {
											value: 8,
											message: "Ký tự không đủ cho địa chỉ email",
										},
										maxLength: {
											value: 120,
											message: "Ký tự vượt quá ký tự cho phép dành cho địa chỉ email",
										},
										pattern: {
											value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i,
											message: "Vui lòng kiểm tra lại các ký tự không đúng cho email",
										},
									})}
								/>
								<ErrorMessage
									errors={errors}
									name="email"
									render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
								/>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="phoneNumber">
									Số điện thoại
								</label>
								<input
									className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
										errors?.phoneNumber && "border-red-500"
									}`}
									id="phoneNumber"
									type="number"
									placeholder="Số điện thoại"
									{...register("phoneNumber", {
										required: {
											value: true,
											message: "Vui lòng nhập số điện thoại",
										},
										minLength: {
											value: 8,
											message: "Số điện thoại không đủ số",
										},
										maxLength: {
											value: 12,
											message: "Số điện thoại quá dài",
										},
									})}
								/>
								<ErrorMessage
									errors={errors}
									name="phoneNumber"
									render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
								/>
							</div>
							<div className="mb-4 md:flex md:justify-between">
								<div className="mb-4 md:mr-2 md:mb-0">
									<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="password">
										Mật khẩu
									</label>
									<input
										className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
											errors?.password && "border-red-500"
										}`}
										id="password"
										type="password"
										placeholder="******************"
										{...register("password", {
											required: {
												value: true,
												message: "Vui lòng nhập mật khẩu",
											},
											minLength: {
												value: 8,
												message: "Mật khẩu quá ngắn",
											},
										})}
									/>
									<ErrorMessage
										errors={errors}
										name="password"
										render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
									/>{" "}
								</div>
								<div className="md:ml-2">
									<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="confirmPassword">
										Xác nhận mật khẩu
									</label>
									<input
										className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
											errors?.confirmPassword && "border-red-500"
										}`}
										id="confirmPassword"
										type="password"
										placeholder="******************"
										{...register("confirmPassword", {
											required: {
												value: true,
												message: "Vui lòng nhập mật khẩu xác nhận",
											},
											minLength: {
												value: 8,
												message: "Mật khẩu xác nhận quá ngắn",
											},
										})}
									/>
									<ErrorMessage
										errors={errors}
										name="confirmPassword"
										render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
									/>
								</div>
							</div>
							<div className="mb-6 text-center">
								<button
									className="w-full px-4 py-2 font-bold text-white bg-blue-500 rounded-full hover:bg-blue-700 focus:outline-none focus:shadow-outline"
									type="submit"
								>
									Đăng ký
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
	);
}

import React, { ReactElement, useState, useEffect } from "react";
import { signOut } from "next-auth/react";
import AdminLayout, { useSessionContext } from "~/src/components/layout/AdminLayout";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import { ApiResponse } from "~/src/pages/types/api.type";
import { REGISTER_API } from "~/src/constants/apis/auth.api";

import "antd/dist/antd.css";
import { Table } from "antd";
import { useForm } from "react-hook-form";
import { ErrorMessage } from "@hookform/error-message";

// interface IFormInput {
// 	label: string;
// 	type: string;
// }
interface IFormInput {
	userName: string;
	fullname: string;
	email: string;
	phoneNumber: string;
	password: string;
	confirmPassword: string;
}

interface UserAccountProps {
	onFinishModal?: () => void;
	onClose: () => void;
}
const UserAccountCreate = (props: UserAccountProps) => {
	const context = useSessionContext();
	const [createSucces, setCreateSucces] = useState<boolean | null>(false);
	const {
		register,
		formState: { errors },
		handleSubmit,
	} = useForm<IFormInput>();
	useEffect(() => {});

	const onFinish = handleSubmit((values: IFormInput) => {
		const password = values.password;
		const confirmPassword = values.confirmPassword;
		if (password !== confirmPassword) return ApiUtil.ToastError("Mật khẩu xác nhận không trùng nhau ! Vui lòng kiểm tra lại");
		props.onFinishModal;

		// ApiUtil.Axios.post<ApiResponse>(REGISTER_API, values)
		// 	.then((res) => {
		// 		if (res.data.success) {
		// 			ApiUtil.ToastSuccess("Tạo mới tài khoản thành công");
		// 			props.onFinishModal;
		// 			setCreateSucces(true);
		// 		} else {
		// 			ApiUtil.ToastError("Tạo mới tài khoản thất bại");
		// 			console.log(res?.data?.message);
		// 		}
		// 	})
		// 	.catch((err) => {
		// 		console.log(err);
		// 	});
	});

	return (
		<>
			<div className="max-w-md mx-auto space-y-6">
				<form onSubmit={onFinish}>
					<label className="text-sm font-bold opacity-70" htmlFor="fullName">
						Họ và tên
					</label>
					<input type="text" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" id="fullName" placeholder="Họ và tên" />
					<label className=" text-sm font-bold opacity-70" htmlFor="email">
						Email
					</label>
					<input
						type="email"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded"
						id="email"
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
					<label className=" text-sm font-bold opacity-70">Tên tài khoản</label>
					<input
						type="text"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded"
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
					<label className=" text-sm font-bold opacity-70">Số điện thoại</label>
					<input
						type="number"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded"
						id="phoneNumber"
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
					<label className=" text-sm font-bold opacity-70">Mật khẩu</label>
					<input
						type="password"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded"
						id="password"
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
					<label className=" text-sm font-bold opacity-70">Xác nhận mật khẩu</label>
					<input
						type="password"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded"
						id="confirmPassword"
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
					<div className="mb-6 text-center">
						<button
							className="w-full px-4 py-2 font-bold text-white bg-blue-500 rounded-full hover:bg-blue-700 focus:outline-none focus:shadow-outline"
							type="submit"
						>
							Đăng ký
						</button>
					</div>
				</form>
				<button onClick={() => props?.onClose()}>Test</button>
			</div>
		</>
	);
};

export default UserAccountCreate;

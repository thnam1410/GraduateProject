import React from "react";
import { Button, Form, Input } from "antd";
import "antd/dist/antd.css";
import { useRouter } from "next/router";
import { ApiUtil } from "~/utils/ApiUtil";
import { ApiResponse } from "~/types/api.type";
import { LOGIN_API, REGISTER_API } from "~/constants/apis/auth.api";
import { NextPage } from "next";

interface IFormInput {
	userName: string;
	email: string;
	phoneNumber: string;
	password: string;
	confirmPassword: string;
	fullName: string;
}

const Register: NextPage = () => {
	const router = useRouter();

	const onFinishFailed = (errorInfo: any) => {
		console.log("Failed:", errorInfo);
	};

	const onRedirectLogin = () => router.push("/login");

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
		<div className="min-h-full flex items-center justify-center py-12 px-4 sm:px-6 lg:px-8">
			<div className="max-w-md w-full space-y-8">
				<div>
					<img
						className="mx-auto h-12 w-auto"
						src="https://tailwindui.com/img/logos/workflow-mark-indigo-600.svg"
						alt="Workflow"
					/>
					<h2 className="mt-6 text-center text-3xl font-extrabold text-gray-900">Đăng nhập</h2>
				</div>
				<Form name="basic" initialValues={{}} onFinish={onFinish} onFinishFailed={onFinishFailed} autoComplete="off">
					<Form.Item name="username" rules={[{ required: true, message: "Vui lòng nhập tên tài khoản!" }]}>
						<Input placeholder={"Tên tài khoản"} />
					</Form.Item>

					<Form.Item name="email" rules={[{ required: true, message: "Vui lòng nhập email!", type: "email" }]}>
						<Input placeholder={"Email"} type={"email"} />
					</Form.Item>

					<Form.Item name="fullName" rules={[{ required: true, message: "Vui lòng nhập tên đầy đủ!" }]}>
						<Input placeholder={"Tên đầy đủ"} type={"text"} />
					</Form.Item>

					<Form.Item
						name="phoneNumber"
						rules={[
							{ required: true, message: "Vui lòng số điện thoại!", pattern: new RegExp("(84|0[3|5|7|8|9])+([0-9]{8})") },
						]}
					>
						<Input placeholder={"Số điện thoại"} type={"text"} />
					</Form.Item>

					<Form.Item name="password" rules={[{ required: true, message: "Vui lòng nhập mật khẩu!" }]}>
						<Input.Password placeholder={"Mật khẩu"} />
					</Form.Item>

					<Form.Item name="confirmPassword" rules={[{ required: true, message: "Vui lòng xác nhận mật khẩu!" }]}>
						<Input.Password placeholder={"Nhập lại mật khẩu"} />
					</Form.Item>

					<Form.Item>
						<div className={"flex items-center justify-around"}>
							<Button type="primary" htmlType="submit">
								Đăng ký
							</Button>
							<Button type="primary" danger onClick={onRedirectLogin}>
								Đăng nhập
							</Button>
						</div>
					</Form.Item>
				</Form>
			</div>
		</div>
	);
};

export default Register;

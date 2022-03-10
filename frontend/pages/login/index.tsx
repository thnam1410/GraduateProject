import React from "react";
import { Button, Form, Input } from "antd";
import "antd/dist/antd.css";
import useMergeState from "~/hooks/useMergeState";
import { LOGIN_API } from "~/constants/apis/auth.api";
import { ApiUtil } from "~/utils/ApiUtil";
import { ApiResponse } from "~/types/api.type";
import { useRouter } from "next/router";
import { NextPage } from "next";

interface IFormInput {
	userName: string;
	password: string;
}
interface IState {
	errorMessage: string;
}
const LoginPage: NextPage = () => {
	const [state, setState] = useMergeState<IState>({
		errorMessage: "",
	});
	const router = useRouter();
	const onFinish = (values: IFormInput) => {
		ApiUtil.Axios.post<ApiResponse>(LOGIN_API, values, { withCredentials: true })
			.then((res) => {
				if (res.data.success) {
					ApiUtil.ToastSuccess("Đăng nhập thành công");
					router.push("/");
				} else {
					ApiUtil.ToastError("Đăng nhập thất bại! Vui lòng thử lại");
				}
			})
			.catch((err) => {
				console.log(err);
			});
	};

	const onFinishFailed = (errorInfo: any) => {
		console.log("Failed:", errorInfo);
	};

	const onRedirectRegister = () => router.push("/register");
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
				<Form
					name="basic"
					initialValues={{ remember: true }}
					onFinish={onFinish}
					onFinishFailed={onFinishFailed}
					autoComplete="off"
				>
					<Form.Item name="username" rules={[{ required: true, message: "Vui lòng nhập tên tài khoản!" }]}>
						<Input placeholder={"Tên tài khoản"} />
					</Form.Item>

					<Form.Item name="password" rules={[{ required: true, message: "Vui lòng nhập mật khẩu!" }]}>
						<Input.Password placeholder={"Mật khẩu"} />
					</Form.Item>
					<span className="text-red-400">{state.errorMessage}</span>
					<Form.Item>
						<div className={"flex items-center justify-around"}>
							<Button type="primary" htmlType="submit">
								Đăng nhập
							</Button>
							<Button type="primary" danger onClick={onRedirectRegister}>
								Đăng ký
							</Button>
						</div>
					</Form.Item>
				</Form>
			</div>
		</div>
	);
};

export default LoginPage;

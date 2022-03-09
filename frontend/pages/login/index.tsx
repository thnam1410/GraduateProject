import React, { ReactNode } from "react";
import { Form, Input, Button, Checkbox, notification } from "antd";
import "antd/dist/antd.css";
import useMergeState from "~/hooks/useMergeState";
import NotifyUtils from "~/utils/NotifyUtils";
import { CHECK_LOGIN_API, LOGIN_API } from "~/constants/apis/auth.api";
import axios from "axios";
import AxiosClient from "~/utils/ApiUtil";
import { TEST_API } from "~/constants/apis/test.api";

interface IFormInput {
	userName: string;
	password: string;
}
interface IState {
	errorMessage: string;
}
const LoginPage = () => {
	const [state, setState] = useMergeState<IState>({
		errorMessage: "",
	});
	const onFinish = (values: IFormInput) => {
		console.log("Success:", values);
		AxiosClient.post(LOGIN_API, values, { withCredentials: true })
			.then((res) => {
				console.log(res);
			})
			.catch((err) => {
				console.log(err);
			});
	};

	const onFinishFailed = (errorInfo: any) => {
		console.log("Failed:", errorInfo);
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
					<Form.Item wrapperCol={{ offset: 10 }}>
						<Button type="primary" htmlType="submit">
							Submit
						</Button>
					</Form.Item>
				</Form>
				<button
					onClick={async () => {
						const res = await AxiosClient.get(CHECK_LOGIN_API).catch((err) => console.log(err));
						console.log("res", res);
					}}
				>
					AA
				</button>
				<div className="class-container" style={{width: 200, height: 200}}>
					<div>a</div>
				</div>
			</div>
		</div>
	);
};

export default LoginPage;

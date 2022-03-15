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
		<div>Dang ky</div>
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

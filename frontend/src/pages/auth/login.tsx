import { Button } from "@paljs/ui/Button";
import { useForm } from "react-hook-form";
import { InputGroup } from "@paljs/ui/Input";
import { Checkbox } from "@paljs/ui/Checkbox";
import React from "react";
import Link from "next/link";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import Auth, { Group } from "../../components/Auth";
import Socials from "../../components/Auth/Socials";
import Layout from "../../Layout";
import { LOGIN_API, CHECK_LOGIN_API, LOGOUT_API } from "~/src/constants/apis/auth.api";
import { useRouter } from "next/router";
import { ApiResponse } from "~/src/pages/types/api.type";

import { NextPage } from "next";

interface IFormInput {
	userName: string;
	password: string;
}

export default function Login() {
	const onCheckbox = () => {
		// v will be true or false
	};
	const router = useRouter();
	const { register, handleSubmit, reset, getValues } = useForm<IFormInput>();

	const onFinish = handleSubmit((values) => {
		const multipleValues = getValues(["userName", "password"]);
		let valueLogin: IFormInput = {
			userName: multipleValues[0],
			password: multipleValues[1],
		};
		ApiUtil.Axios.post<ApiResponse>(LOGIN_API, valueLogin, { withCredentials: true })
			.then((res) => {
				console.log("check Re", res);
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
	});
	return (
		<Layout title="Đăng nhập">
			<Auth title="Đăng nhập" subTitle="">
				<form name="form" onSubmit={onFinish}>
					<InputGroup fullWidth>
						<input {...register("userName")} type="text" id="email" name="email" placeholder="Email Address" />
					</InputGroup>
					<InputGroup fullWidth>
						<input {...register("password")} type="password" id="password" name="password" placeholder="Password" />
					</InputGroup>
					<Group>
						<Checkbox checked onChange={onCheckbox}>
							Lưu mật khẩu
						</Checkbox>
						<Link href="/auth/request-password">
							<a>Quên mật khẩu ?</a>
						</Link>
					</Group>
					<Button status="Success" type="submit" shape="SemiRound" fullWidth>
						Đăng nhập
					</Button>
				</form>
				{/* <Socials /> */}
				<p>
					Bạn chưa có tài khoản?{" "}
					<Link href="/auth/register">
						<a>Đăng ký</a>
					</Link>
				</p>
			</Auth>
		</Layout>
	);
}

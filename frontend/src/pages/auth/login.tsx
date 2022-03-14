import { Button } from "@paljs/ui/Button";
import { useForm } from "react-hook-form";
import { InputGroup } from "@paljs/ui/Input";
import { Checkbox } from "@paljs/ui/Checkbox";
import React from "react";
import Link from "next/link";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import Auth, { Group } from "../../components/Auth";
import Layout from "../../Layout";
import { LOGIN_API } from "~/src/constants/apis/auth.api";
import { useRouter } from "next/router";
import { ApiResponse } from "~/src/pages/types/api.type";

interface IFormInput {
	userName: string;
	password: string;
}

export default function Login() {
	const router = useRouter();
	const { register, handleSubmit, reset, getValues } = useForm<IFormInput>();

	const onFinish = handleSubmit((values: IFormInput) => {
		ApiUtil.Axios.post<ApiResponse>(LOGIN_API, values, { withCredentials: true })
			.then((res) => {
				console.log("check Re", res);
				if (res.data.success) {
					ApiUtil.ToastSuccess("Đăng nhập thành công");
					router.push("/admin/account/user-account");
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
						<input {...register("userName")} type="text" id="userName" placeholder="Username" />
					</InputGroup>
					<InputGroup fullWidth>
						<input {...register("password")} type="password" id="password" placeholder="Password" />
					</InputGroup>
					<Group>
						<Checkbox checked onChange={() => console.log("")}>
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

import { Button } from "@paljs/ui/Button";
import { InputGroup } from "@paljs/ui/Input";
import { Checkbox } from "@paljs/ui/Checkbox";
import React from "react";
import Link from "next/link";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import Auth, { Group } from "../../components/Auth";
import Socials from "../../components/Auth/Socials";
import Layout from "../../Layout";
import { CHECK_LOGIN_API, LOGOUT_API } from "~/src/constants/apis/auth.api";

export default function Login() {
	const onCheckbox = () => {
		// v will be true or false
	};
	return (
		<Layout title="Đăng nhập">
			<Auth title="Đăng nhập" subTitle="">
				<form>
					<InputGroup fullWidth>
						<input type="email" placeholder="Email Address" />
					</InputGroup>
					<InputGroup fullWidth>
						<input type="password" placeholder="Password" />
					</InputGroup>
					<Group>
						<Checkbox checked onChange={onCheckbox}>
							Lưu mật khẩu
						</Checkbox>
						<Link href="/auth/request-password">
							<a>Quên mật khẩu ?</a>
						</Link>
					</Group>
					<Button
						onClick={async () => {
							await ApiUtil.Axios.get(CHECK_LOGIN_API).then((res: any) => console.log("res", res?.data?.result));
						}}
						status="Success"
						type="button"
						shape="SemiRound"
						fullWidth
					>
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

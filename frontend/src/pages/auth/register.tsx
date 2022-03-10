import { Button } from "@paljs/ui/Button";
import { InputGroup } from "@paljs/ui/Input";
import { Checkbox } from "@paljs/ui/Checkbox";
import React from "react";
import styled from "styled-components";
import Link from "next/link";
import Auth from "../../components/Auth";
import Layout from "../../Layout";
import Socials from "../../components/Auth/Socials";

const Input = styled(InputGroup)`
	margin-bottom: 2rem;
`;

export default function Register() {
	const onCheckbox = () => {
		// v will be true or false
	};
	return (
		<Layout title="Đăng ký">
			<Auth title="Đăng ký">
				<form>
					<Input fullWidth>
						<input type="text" placeholder="Tên tài khoản" />
					</Input>
					<Input fullWidth>
						<input type="email" placeholder="Địa chỉ Email" />
					</Input>
					<Input fullWidth>
						<input type="password" placeholder="Mật khẩu" />
					</Input>
					<Input fullWidth>
						<input type="password" placeholder="Xác nhận mật khẩu" />
					</Input>
					<Input fullWidth>
						<input type="number" placeholder="Số điện thoại" />
					</Input>
					<Input fullWidth>
						<input type="text" placeholder="Mô tả dịch vụ" />
					</Input>
					<Input fullWidth>
						<input type="text" placeholder="Thành viên giới thiệu" />
					</Input>
					<Button status="Success" type="button" shape="SemiRound" fullWidth>
						Đăng ký
					</Button>
				</form>
				<p>
					Bank đã có tài khoản?{" "}
					<Link href="/auth/login">
						<a>Đăng nhập</a>
					</Link>
				</p>
			</Auth>
		</Layout>
	);
}

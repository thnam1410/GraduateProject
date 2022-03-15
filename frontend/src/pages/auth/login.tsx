import { useForm } from "react-hook-form";
import React from "react";
import { useRouter } from "next/router";
import { signIn, useSession } from "next-auth/react";
import { SignInResponse } from "next-auth/react/types";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import { UserSession } from "~/src/types/UserInfo";
import { Role } from "~/src/constants/constants";
import { ErrorMessage } from "@hookform/error-message";
import Link from 'next/link'

interface IFormInput {
	userName: string;
	password: string;
}
export default function Login() {
	const router = useRouter();
	const session = useSession();
	const {
		register,
		handleSubmit,
		formState: { errors },
	} = useForm<IFormInput>({
		defaultValues: {
			userName: "",
			password: "",
		},
	});
	const onFinish = handleSubmit(async (values: IFormInput) => {
		const { userName, password } = values;
		const res: SignInResponse | undefined = await signIn("credentials", {
			userName,
			password,
			redirect: false,
		});
		if (res!.ok) {
			if (session?.data?.user) {
				const userSession = session.data.user as UserSession;
				if (userSession.rights.includes(Role.ADMIN)) {
					return router.push("/admin");
				}
				return router.push("/");
			} else {
				ApiUtil.ToastError("Có lỗi xảy ra vui lòng liên hệ quản trị viên!");
			}
		} else {
			if (res!.error) {
				ApiUtil.ToastError(res!.error);
			}
		}
	});
	return (
		<div className="container mx-auto">
			<div className="flex justify-center px-6 my-12">
				<div className="w-full xl:w-3/4 lg:w-11/12 flex">
					<div
						className="w-full h-auto bg-gray-400 hidden lg:block lg:w-1/2 bg-cover rounded-l-lg"
						style={{ backgroundImage: "url('https://source.unsplash.com/K4mSJ7kc0As/600x800')" }}
					/>
					<div className="w-full lg:w-1/2 bg-white p-5 rounded-lg lg:rounded-l-none">
						<h3 className="pt-4 text-2xl text-center">Đăng nhập!</h3>
						<form className="px-8 pt-6 pb-8 mb-4 bg-white rounded" onSubmit={onFinish}>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="userName">
									Tên tài khoản
								</label>
								<input
									className={`w-full px-3 py-2 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
										errors?.password && "border-red-500"
									}`}
									id="userName"
									type="text"
									placeholder="Username"
									{...register("userName", {
										required: "Vui lòng nhập tên tài khoản",
										minLength: { value: 5, message: "Tối thiểu 5 kí tự" },
									})}
								/>
								<ErrorMessage
									errors={errors}
									name="userName"
									render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
								/>
							</div>
							<div className="mb-4">
								<label className="block mb-2 text-sm font-bold text-gray-700" htmlFor="password">
									Mật khẩu
								</label>
								<input
									className={`w-full px-3 py-2 mb-3 text-sm leading-tight text-gray-700 border rounded shadow appearance-none focus:outline-none focus:shadow-outline ${
										errors?.password && "border-red-500"
									}`}
									id="password"
									type="password"
									placeholder="******************"
									{...register("password", {
										required: "Vui lòng nhập mật khẩu",
									})}
								/>
								<ErrorMessage
									errors={errors}
									name="password"
									render={({ message }) => (message ? <p className="text-red-400">{message}</p> : null)}
								/>
								{/*<p className="text-xs italic text-red-500">Please choose a password.</p>*/}
							</div>
							{/*<div className="mb-4">*/}
							{/*	<input className="mr-2 leading-tight" type="checkbox" id="checkbox_id" />*/}
							{/*	<label className="text-sm" htmlFor="checkbox_id">*/}
							{/*		Remember Me*/}
							{/*	</label>*/}
							{/*</div>*/}
							<div className="mb-6 text-center">
								<button
									className="w-full px-4 py-2 font-bold text-white bg-blue-500 rounded-full hover:bg-blue-700 focus:outline-none focus:shadow-outline"
									type="submit"
								>
									Đăng nhập
								</button>
							</div>
							<hr className="mb-6 border-t" />
							<div className="text-center">
								<Link href={"/auth/register"}>
									<a className="inline-block text-sm text-blue-500 align-baseline hover:text-blue-800">Tạo tài khoản</a>
								</Link>
							</div>
							<div className="text-center">
								<a className="inline-block text-sm text-blue-500 align-baseline hover:text-blue-800">
									Quên mất khẩu
								</a>
							</div>
						</form>
					</div>
				</div>
			</div>
		</div>
	);
}
// <div className="h-screen w-screen flex flex-col items-center justify-center">
{
	/*<h1 style={{fontSize: 30}}>Đăng nhập</h1>*/
}
{
	/*<Form*/
}
{
	/*	name="basic"*/
}
{
	/*	labelCol={{ span: 8 }}*/
}
{
	/*	wrapperCol={{ span: 16 }}*/
}
{
	/*	initialValues={{ remember: true }}*/
}
{
	/*	onFinish={onFinish}*/
}
{
	/*	onFinishFailed={onFinishFailed}*/
}
{
	/*	autoComplete="off"*/
}
{
	/*>*/
}
{
	/*	<Form.Item label="Username" name="username" rules={[{ required: true, message: "Please input your username!" }]}>*/
}
{
	/*		<Input />*/
}
{
	/*	</Form.Item>*/
}

{
	/*	<Form.Item label="Password" name="password" rules={[{ required: true, message: "Please input your password!" }]}>*/
}
{
	/*		<Input.Password />*/
}
{
	/*	</Form.Item>*/
}

{
	/*	<Form.Item name="remember" valuePropName="checked" wrapperCol={{ offset: 8, span: 16 }}>*/
}
{
	/*		<Checkbox>Remember me</Checkbox>*/
}
{
	/*	</Form.Item>*/
}

{
	/*	<Form.Item wrapperCol={{ offset: 8, span: 16 }}>*/
}
{
	/*		<Button type="primary" htmlType="submit">*/
}
{
	/*			Submit*/
}
{
	/*		</Button>*/
}
{
	/*	</Form.Item>*/
}
{
	/*</Form>*/
}
{
	/*</div>*/
}

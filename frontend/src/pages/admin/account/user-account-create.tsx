import React, { ReactElement } from "react";
import { signOut } from "next-auth/react";
import AdminLayout, { useSessionContext } from "~/src/components/layout/AdminLayout";
import "antd/dist/antd.css";
import { Table } from "antd";

interface IFormInput {
	label: string;
	type: string;
}
const UserAccountCreate = () => {
	const context = useSessionContext();
	const infoInputForm: IFormInput[] = [
		{
			label: "Họ và tên",
			type: "text",
		},
		{
			label: "Email",
			type: "email",
		},
		{
			label: "Họ và tên",
			type: "text",
		},
	];

	return (
		<>
			<div className="max-w-md mx-auto space-y-6">
				<form action="">
					<label className="className text-sm font-bold opacity-70">Họ và tên</label>
					<input
						type="text"
						className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded border-2 border-slate-200 focus:border-slate-600 focus:outline-none"
					/>
					<label className="uppercase text-sm font-bold opacity-70">Email</label>
					<input type="email" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" />
					<label className="uppercase text-sm font-bold opacity-70">Tên tài khoản</label>
					<input type="text" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" />
					<label className="uppercase text-sm font-bold opacity-70">Số điện thoại</label>
					<input type="number" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" />
					<label className="uppercase text-sm font-bold opacity-70">Mật khẩu</label>
					<input type="password" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" />
					<label className="uppercase text-sm font-bold opacity-70">Xác nhận mật khẩu</label>
					<input type="password" className="p-3 mt-2 mb-4 w-full bg-slate-200 rounded" />
				</form>
			</div>
		</>
	);
};

export default UserAccountCreate;
{
	/* <select className="w-full p-3 mt-2 mb-4 w-full bg-slate-200 rounded border-2 border-slate-200 focus:border-slate-600 focus:outline-none">
						<option value="">Javascript</option>
						<option value="">Ruby</option>
						<option value="">Python</option>
						<option value="">PHP</option>
						<option value="">Java</option>
					</select> */
}
{
	/* <input
						type="submit"
						className="py-3 px-6 my-2 bg-emerald-500 text-white font-medium rounded hover:bg-indigo-500 cursor-pointer ease-in-out duration-300"
						value="Send"
					/> */
}

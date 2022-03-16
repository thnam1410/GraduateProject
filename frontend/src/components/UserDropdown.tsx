import React from "react";
import {Avatar, Popover} from "antd";
import {UserOutlined} from "@ant-design/icons";
import {useSessionContext} from "~/src/components/layout/AdminLayout";
import {signOut} from "next-auth/react";

const UserDropdown = () => {
	const userContext = useSessionContext()
	const renderContent = () => {
		return <div>
			<button className='focus:outline-none' onClick={() => signOut()}>Đăng xuất</button>
		</div>
	}
	return (
		<>
			<div className="text-blueGray-500 block cursor-pointer">
				<div className="items-center flex">
					<span className="w-12 h-12 text-sm text-white bg-blueGray-200 inline-flex items-center justify-center rounded-full">
						{/*<img alt="..." className="w-full rounded-full align-middle border-none shadow-lg" src="/img/team-1-800x800.jpg" />*/}
						<Popover placement="topLeft" title={`Tài khoản: ${userContext?.userInfo?.user?.userName}`} content={renderContent()} trigger="click">
							<Avatar icon={<UserOutlined />} />
						</Popover>
					</span>
				</div>
			</div>
		</>
	);
};

export default UserDropdown;

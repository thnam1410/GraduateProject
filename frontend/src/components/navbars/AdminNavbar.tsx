import React, { RefObject } from "react";
import UserDropdown from "~/src/components/UserDropdown";
import { MenuFoldOutlined, MenuUnfoldOutlined } from "@ant-design/icons";
import { Layout } from "antd";
import { SideBarRef } from "~/src/components/layout/AdminLayout";

interface Props {
	sideBarRef: RefObject<SideBarRef>;
}
const { Header } = Layout;
export default function AdminNavbar(props: Props) {
	const { sideBarRef } = props;
	return (
		<div className="w-full h-auto" style={{ background: "red" }}>
			<Header style={{ background: "#fff", padding: 0, height: 70, borderBottom: "1px solid #e5e7eb" }}>
				<div className="flex items-center">
					{React.createElement(sideBarRef.current?.collapsed ? MenuUnfoldOutlined : MenuFoldOutlined, {
						className: "px-[24px] py-0 text-[18px] cursor-pointer",
						onClick: () => sideBarRef.current?.setCollapsed(!sideBarRef.current?.collapsed),
					})}
					<div className="flex-1 flex justify-end pr-4 pt-2">
						<div>
							<UserDropdown />
						</div>
					</div>
				</div>
			</Header>
		</div>
	);
}

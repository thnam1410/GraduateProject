import React, { forwardRef, ReactNode, useImperativeHandle } from "react";
import { useRouter } from "next/router";
import { Layout, Menu } from "antd";
import { HomeOutlined, UploadOutlined, UserOutlined, VideoCameraOutlined } from "@ant-design/icons";
import "antd/dist/antd.css";
const { SubMenu } = Menu;

const { Sider } = Layout;

const SideBar = forwardRef((props, ref) => {
	const [collapsed, setCollapsed] = React.useState(false);
	const router = useRouter();
	const toggle = () => {
		setCollapsed(!collapsed);
	};
	useImperativeHandle(
		ref,
		() => ({
			setCollapsed,
			collapsed,
		}),
		[collapsed]
	);

	const renderMenu = (arrMenu: MenuListType[] = []): ReactNode => {
		return arrMenu.map(({ key, icon, title, isSubMenu, children }) => {
			if (isSubMenu) {
				return (
					<SubMenu key={key} icon={icon} title={title}>
						{renderMenu(children)}
					</SubMenu>
				);
			}
			return (
				<Menu.Item key={key} icon={icon}>
					{title}
				</Menu.Item>
			);
		});
	};

	return (
		<Sider
			className="h-screen"
			style={{ borderRight: "1px solid grey" }}
			trigger={null}
			collapsible
			collapsed={collapsed}
			theme={"light"}
		>
			<div
				className="flex justify-center items-center text-left text-blueGray-600 mr-0 inline-block whitespace-nowrap text-lg uppercase font-bold p-4 px-4 cursor-pointer mb-5"
				style={{ background: "rgba(255, 255, 255, 0.3)", height: 70, borderBottom: "1px solid #e5e7eb" }}
				onClick={() => router.push("admin")}
			>
				{collapsed ? <HomeOutlined /> : "Real Estate"}
			</div>
			<Menu theme="light" mode="inline" defaultSelectedKeys={["1"]}>
				{renderMenu(menuList)}
			</Menu>
		</Sider>
	);
});
type MenuListType = {
	key: string | number;
	icon: ReactNode;
	title: ReactNode;
	isSubMenu?: boolean;
	children?: MenuListType[];
};
const menuList: MenuListType[] = [
	{
		key: "1",
		title: "nav 1",
		icon: <UserOutlined />,
	},
	{
		key: "2",
		title: "nav 2",
		icon: <VideoCameraOutlined />,
	},
	{
		key: "3",
		title: "nav 3",
		icon: <UploadOutlined />,
	},
	{
		key: "sub1",
		title: "sub menu",
		isSubMenu: true,
		icon: <UploadOutlined />,
		children: [
			{
				key: "4",
				title: "nav 1",
				icon: <UserOutlined />,
			},
			{
				key: "5",
				title: "nav 2",
				icon: <VideoCameraOutlined />,
			},
		],
	},
];
SideBar.displayName = "SideBar";
export default SideBar;

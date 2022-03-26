import React, { forwardRef, ReactNode, useImperativeHandle } from "react";
import { useRouter } from "next/router";
import { Layout, Menu } from "antd";
import {
	HomeOutlined,
	UploadOutlined,
	UserOutlined,
	VideoCameraOutlined,
	KeyOutlined,
	UnorderedListOutlined,
	GiftOutlined,
	UsergroupAddOutlined,
} from "@ant-design/icons";
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
		return arrMenu.map(({ key, icon, title, isSubMenu, children, link }) => {
			if (isSubMenu) {
				return (
					<SubMenu key={key} icon={icon} title={title}>
						{renderMenu(children)}
					</SubMenu>
				);
			}
			return (
				<Menu.Item key={key} icon={icon} onClick={() => router.push(link || "")}>
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
	icon?: ReactNode;
	title: ReactNode;
	isSubMenu?: boolean;
	children?: MenuListType[];
	link?: string;
};
const menuList: MenuListType[] = [
	{
		key: "1",
		title: "Quản lý tài khoản",
		icon: <UserOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "1.1",
				title: "Tài khoản",
				// icon: <UserOutlined />,
				link: "/admin/account/user-account",
			},
		],
	},
	{
		key: "2",
		title: "Quản lý phân quyền",
		icon: <KeyOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "2.2",
				title: "Quyền tài khoản",
				link: "/admin/role/System-Role",
			},
		],
	},
	{
		key: "3",
		title: "Quản lý bài viết",
		icon: <UnorderedListOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "3.1",
				title: "Bài đăng bất động sản",
				link: "/admin/manageRealEstate/Real-Estate",
			},
			{
				key: "3.2",
				title: "Bài đăng",
				link: "/admin/manageRealEstate/post",
			},
			{
				key: "3.3",
				title: "Dự án",
				link: "/admin/manageRealEstate/project",
			},
		],
	},
	{
		key: "4",
		title: "Gói ưu đãi",
		icon: <GiftOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "4.1",
				title: "Ưu đãi",
				link: "/admin/manageOfferPackage/Offer-Package",
			},
		],
	},

	{
		key: "5",
		title: "Quản lý người bán",
		icon: <UsergroupAddOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "5.1",
				title: "Người bán",
				link: "/admin/manageSalePerson/Sale-Person",
			},
		],
	},
	// {
	// 	key: "2",
	// 	title: "nav 2",
	// 	icon: <VideoCameraOutlined />,
	// },
	// {
	// 	key: "3",
	// 	title: "nav 3",
	// 	icon: <UploadOutlined />,
	// },
	// {
	// 	key: "sub1",
	// 	title: "sub menu",
	// 	isSubMenu: true,
	// 	icon: <UploadOutlined />,
	// children: [
	// 	{
	// 		key: "4",
	// 		title: "nav 1",
	// 		icon: <UserOutlined />,
	// 	},
	// 	{
	// 		key: "5",
	// 		title: "nav 2",
	// 		icon: <VideoCameraOutlined />,
	// 	},
	// ],
	// },
];
SideBar.displayName = "SideBar";
export default SideBar;

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
	UsergroupAddOutlined, SettingOutlined,
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
			width={250}
		>
			<div
				className="flex justify-center items-center text-left text-blueGray-600 mr-0 inline-block whitespace-nowrap text-lg uppercase font-bold p-4 px-4 cursor-pointer mb-5"
				style={{ background: "rgba(255, 255, 255, 0.3)", height: 70, borderBottom: "1px solid #e5e7eb" }}
				onClick={() => router.push("/admin")}
			>
				{collapsed ? <HomeOutlined /> : "Real Estate"}
			</div>
			<Menu theme="light" mode="inline" defaultSelectedKeys={[router.pathname.toString()]}>
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
		title: "Qu???n l?? t??i kho???n",
		icon: <UserOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "1.1",
				title: "T??i kho???n",
				// icon: <UserOutlined />,
				link: "/admin/account/user-account",
			},
		],
	},
	{
		key: "2",
		title: "Qu???n l?? ph??n quy???n",
		icon: <KeyOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "2.2",
				title: "Quy???n t??i kho???n",
				link: "/admin/role/System-Role",
			},
		],
	},
	{
		key: "3",
		title: "Qu???n l?? b??i vi???t",
		icon: <UnorderedListOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "3.1",
				title: "B??i ????ng b???t ?????ng s???n",
				link: "/admin/manage-real-estate/Real-Estate",
			},
			{
				key: "3.2",
				title: "B??i ????ng",
				link: "/admin/manage-real-estate/post",
			},
			{
				key: "3.3",
				title: "D??? ??n",
				link: "/admin/manage-real-estate/project",
			},
		],
	},
	{
		key: "4",
		title: "G??i ??u ????i",
		icon: <GiftOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "4.1",
				title: "??u ????i",
				link: "/admin/manage-offer-package/Offer-Package",
			},
		],
	},
	{
		key: "5",
		title: "Qu???n l?? ng?????i b??n",
		icon: <UsergroupAddOutlined />,
		isSubMenu: true,
		children: [
			{
				key: "5.1",
				title: "Ng?????i b??n",
				link: "/admin/manage-sale-person/Sale-Person",
			},
		],
	},
	{
		key: "/admin/masterdata",
		title: "Danh m???c",
		icon: <SettingOutlined/>,
		link: "/admin/masterdata"
	}
];
SideBar.displayName = "SideBar";
export default SideBar;

import React, { ReactElement, useState, useEffect } from "react";
import { signOut } from "next-auth/react";
import AdminLayout, { useSessionContext } from "~/src/components/layout/AdminLayout";
import "antd/dist/antd.css";
import { Table, Modal, Button, Tooltip } from "antd";
import UserAccountCreate from "../../../components/pages/admin/account/user-account-create";
import { ExclamationCircleOutlined, DiffOutlined } from "@ant-design/icons";
import CustomModal, { ModalRef } from "~/src/components/CustomModal/CustomModal";
import UserAccountColumn from "~/src/components/ColumnComponent/UserAccountColumn";
import { ApiUtil } from "~/src/utils/ApiUtil";
import { ApiResponse } from "~/src/types/api.type";
import { LOAD_USER_ACCOUNT } from "~/src/constants/apis/auth.api";
import { UserAccountListDto, UserAccountTable } from "~/src/types/UserAccountListDto";
import GridButtonBase from "~/src/components/ButtonBase/GridButtonBase";
import ControllerNumberPost from "~/src/components/pages/admin/account/controller-number-post";

const UserAccount = () => {
	const { confirm } = Modal;
	const modalRef = React.useRef<ModalRef>(null);
	const context = useSessionContext();
	const [data, setData] = useState<UserAccountTable[]>();

	const columns = [
		...UserAccountColumn.columns,
		{
			title: "Hành động",
			dataIndex: "phoneNumberConfirmed",
			key: "phoneNumberConfirmed",
			width: 300,
			minWidth: 120,
			render: (text: any, record: any) => (
				<div className="flex items-center justify-center">
					{/* <GridButtonBase type={"edit"} onClick={() => onEdit()} /> */}
					<Tooltip title={"Kiểm soát số lượng bài viết"}>
						<button
							type="button"
							className="text-white bg-gradient-to-r from-blue-400 via-blue-500 to-blue-600 hover:bg-gradient-to-br  focus:outline-none focus:ring-blue-300 dark:focus:ring-blue-700 font-medium rounded-lg text-sm px-4 py-2 text-center mr-2 mb-2"
							onClick={onDetailPost}
						>
							<DiffOutlined />
						</button>
					</Tooltip>
					<GridButtonBase type={"delete"} onClick={() => {}} />
				</div>
			),
		},
	];

	useEffect(() => {
		loadData();
	});

	const loadData = () => {
		// 2 là như này
		ApiUtil.Axios.get<ApiResponse>(LOAD_USER_ACCOUNT)
			.then((res) => {
				if (res.data.success) {
					const dataApi = res.data.result as UserAccountListDto[];
					let dataTable = dataApi.map((item) => ({
						fullName: item.fullName,
						email: item.email,
						active: item.active,
						userName: item.userName,
						phoneNumber: item.phoneNumber,
						roleId: item.userRoles[0].roleId,
						code: item.userRoles[0].role.code,
					})) as UserAccountTable[];
					setData(dataTable);
				} else {
					ApiUtil.ToastError("Tải dữ liệu thất bại!");
					console.log(res?.data?.message);
				}
			})
			.catch((err) => {
				console.log(err);
			});
	};

	const onCreate = () => {
		modalRef.current?.onOpen(<UserAccountCreate onClose={() => modalRef.current?.onClose()} />, "Tạo mới");
	};

	const onDetailPost = () => {
		modalRef.current?.onOpen(<ControllerNumberPost onClose={() => modalRef.current?.onClose()} />, "Số lượng bài viết");
	};

	const showDeleteConfirm = () => {
		return confirm({
			title: "Bạn có muốn xóa dữ liệu?",
			icon: <ExclamationCircleOutlined />,
			okText: "Đồng ý",
			okType: "danger",
			cancelText: "Quay lại",
			onOk() {
				console.log("OK");
			},
			onCancel() {
				console.log("Cancel");
			},
		});
	};

	return (
		<div>
			<div className="relative h-20 r-0">
				<button className="absolute top-0 right-0 h-10 w-25 bg-blue-500 text-white font-bold py-2 px-4 rounded" onClick={onCreate}>
					{"Tạo mới"}
				</button>
			</div>
			<Table style={{ width: "auto" }} dataSource={data} columns={columns} />
			<CustomModal ref={modalRef} />
		</div>
	);
};

UserAccount.getLayout = function getLayout(page: ReactElement) {
	return <AdminLayout>{page}</AdminLayout>;
};

export default UserAccount;

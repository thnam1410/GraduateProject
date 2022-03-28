import React, { ReactElement, useState, useEffect } from "react";
import { signOut } from "next-auth/react";
import AdminLayout, { useSessionContext } from "~/src/components/layout/AdminLayout";
import "antd/dist/antd.css";
import { Table, Modal, Button } from "antd";
import UserAccountCreate from "../../../components/pages/admin/account/user-account-create";
import { ExclamationCircleOutlined } from "@ant-design/icons";
import CustomModal, { ModalRef } from "~/src/components/CustomModal/CustomModal";
import UserAccountColumn from "~/src/components/ColumnComponent/UserAccountColumn";
import { ApiUtil } from "../../utils/ApiUtil";
import { ApiResponse } from "../../types/api.type";
import { LOAD_USER_ACCOUNT } from "~/src/constants/apis/auth.api";
import { UserAccountListDto, UserAccountTable } from "~/src/types/UserAccountListDto";

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
				<div className="flex">
					<button
						data-tooltip-target="tooltip-dark"
						className="flex-none hover:bg-sky-700 bg-blue-500 text-white font-bold py-2 px-2 rounded mr-2"
						onClick={onCreate}
						type="submit"
					>
						<svg className="w-5 h-5" viewBox="0 0 183.792 183.792">
							<path d="M54.734 9.053C39.12 18.067 27.95 32.624 23.284 50.039c-4.667 17.415-2.271 35.606 6.743 51.22 12.023 20.823 34.441 33.759 58.508 33.759a67.31 67.31 0 0 0 22.287-3.818l30.364 52.592 21.65-12.5-30.359-52.583c10.255-8.774 17.638-20.411 21.207-33.73 4.666-17.415 2.27-35.605-6.744-51.22C134.918 12.936 112.499 0 88.433 0 76.645 0 64.992 3.13 54.734 9.053zm70.556 37.206c5.676 9.831 7.184 21.285 4.246 32.25-2.938 10.965-9.971 20.13-19.802 25.806a42.466 42.466 0 0 1-21.199 5.703c-15.163 0-29.286-8.146-36.857-21.259-5.676-9.831-7.184-21.284-4.245-32.25 2.938-10.965 9.971-20.13 19.802-25.807A42.47 42.47 0 0 1 88.433 25c15.164 0 29.286 8.146 36.857 21.259z" />
						</svg>
					</button>

					<button
						data-tooltip-target="tooltip-dark"
						className="flex-none hover:bg-sky-700 bg-red-500 text-white font-bold py-2 px-2 rounded mr-2"
						onClick={showDeleteConfirm}
						type="button"
					>
						<svg className="w-5 h-5 mr-1">
							<path d="M6 19c0 1.1.9 2 2 2h8c1.1 0 2-.9 2-2V7H6v12zM19 4h-3.5l-1-1h-5l-1 1H5v2h14V4z" />
						</svg>
					</button>
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
					// lúc này cái item tự nó hiểu là 1 thằng DTO
					//còn trc đó m để any thì nó k hiểu
					//đối với trường hợp any thì ko xài dc arrow function =>    .map((item: any) => ...)
					// cái này khai báo sâu như dto dưới BE luôn hả
					//xài cái gì, khai cái đó, k nhất thiết phải copy y chang
					// okay anh iu
					// setData();
					// ApiUtil.ToastSuccess("Đăng ký thành công! Vui lòng đăng nhập");
				} else {
					// ApiUtil.ToastError("Đăng ký thất bại! Vui lòng thử lại");
					// console.log(res?.data?.message);
				}
			})
			.catch((err) => {
				console.log(err);
			});
	};

	const onCreate = () => {
		modalRef.current?.onOpen(<UserAccountCreate onClose={() => modalRef.current?.onClose()} />, "Tạo mới");
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

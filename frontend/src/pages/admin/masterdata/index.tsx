import React, { ReactElement, useEffect, useMemo, useRef, useState } from "react";
import { GetServerSideProps, NextPage } from "next";
import AdminLayout from "~/src/components/layout/AdminLayout";
import TopToolbar from "~/src/components/TopToolbar/TopToolbar";
import { ButtonBaseProps } from "~/src/components/ButtonBase/ButtonBase";
import { ApiUtil } from "~/src/utils/ApiUtil";
import { ApiResponse } from "~/src/types/api.type";
import { MasterData } from "~/src/types/MasterData";
import { MASTER_DATA_INDEX_API } from "~/src/constants/apis/masterdata.api";
import { AxiosRequestHeaders, AxiosResponse } from "axios";
import { Table } from "antd";
import { MasterDataGridColumns } from "~/src/components/pages/admin/masterdata/masterdata.config";
import GridButtonBase from "~/src/components/ButtonBase/GridButtonBase";
import CustomModal, { ModalRef } from "~/src/components/CustomModal/CustomModal";
import MasterDataForm from "~/src/components/pages/admin/masterdata/MasterData.Form";
import { AgGridReact } from "ag-grid-react";
import "ag-grid-community";
import "ag-grid-community/dist/styles/ag-grid.css";
import "ag-grid-community/dist/styles/ag-theme-alpine.css";
import BaseGrid from "~/src/components/BaseGrid/BaseGrid";
interface IProps {
	data: MasterData[];
}
const MasterData: NextPage<IProps> = (props) => {
	const { data } = props;
	const modalRef = useRef<ModalRef>(null);
	const [dataSource, setDataSource] = useState<MasterData[]>(data);
	const topButtons: ButtonBaseProps[] = [
		{
			buttonName: "Tạo mới",
			onClick: () => onSave(null),
			buttonType: "create",
		},
	];

	const onSave = (data: MasterData | null): void => {
		modalRef.current?.onOpen(<MasterDataForm initData={data} onClose={() => modalRef?.current?.onClose()} />, "Tạo mới");
	};
	const onDelete = (): void => {};
	useEffect(() => {
		setDataSource(props.data);
	});

	// const loadData = () => {
	// 	// 2 là như này
	// 	ApiUtil.Axios.get<ApiResponse>(MASTER_DATA_INDEX_API)
	// 		.then((res) => {
	// 			if (res.data.success) {
	// 				const dataApi = res.data.result as MasterData[];
	// 				let dataTable = dataApi.map((item) => ({
	// 					...item,
	// 				})) as MasterData[];
	// 				setDataSource(dataTable);
	// 				console.log("dataSource", dataSource);
	// 			} else {
	// 				ApiUtil.ToastError("Tải dữ liệu thất bại!");
	// 				console.log(res?.data?.message);
	// 			}
	// 		})
	// 		.catch((err) => {
	// 			console.log(err);
	// 		});
	// };

	const getColumnConfig = () => {
		return [
			...MasterDataGridColumns,
			{
				headerName: "Hành động",
				field: "action",
				cellRenderer: (params: any, value: any) => {
					console.log("params", params);
					console.log("value", value);
					return (
						<div className="flex items-center justify-center">
							<GridButtonBase type={"edit"} onClick={() => onSave(params.data)} />
							<GridButtonBase type={"delete"} onClick={() => onDelete()} />
						</div>
					);
				},
			},
		];
	};
	console.log("props", props);
	return (
		<div className="flex flex-col h-full w-full">
			<TopToolbar buttons={topButtons} />
			<div style={{ height: 550 }}>
				<BaseGrid
					className="ag-theme-alpine"
					rowSelection={"multiple"}
					rowGroupPanelShow={"always"}
					pivotPanelShow={"always"}
					columnDefs={getColumnConfig()}
					pagination={true}
					rowData={dataSource}
				/>
			</div>
			<CustomModal ref={modalRef} />
		</div>
	);
};

export default MasterData;

export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	const headers = { Cookie: req.headers.cookie } as AxiosRequestHeaders;
	const response = (await ApiUtil.Axios.get(MASTER_DATA_INDEX_API)) as AxiosResponse<ApiResponse<MasterData[]>>;
	return {
		props: {
			data: response.data.result || [],
		},
	};
};

// @ts-ignore
MasterData.getLayout = function getLayout(page: ReactElement) {
	return <AdminLayout>{page}</AdminLayout>;
};

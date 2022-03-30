import React, {ReactElement, useRef, useState} from "react";
import {GetServerSideProps, NextPage} from "next";
import AdminLayout from "~/src/components/layout/AdminLayout";
import TopToolbar from "~/src/components/TopToolbar/TopToolbar";
import {ButtonBaseProps} from "~/src/components/ButtonBase/ButtonBase";
import {ApiUtil} from "~/src/utils/ApiUtil";
import {ApiResponse} from "~/src/types/api.type";
import {MasterData} from "~/src/types/MasterData";
import {MASTER_DATA_INDEX_API} from "~/src/constants/apis/masterdata.api";
import {AxiosRequestHeaders, AxiosResponse} from "axios";
import {Table} from "antd";
import {MasterDataGridColumns} from "~/src/components/pages/admin/masterdata/masterdata.config";
import GridButtonBase from "~/src/components/ButtonBase/GridButtonBase";
import CustomModal, {ModalRef} from "~/src/components/CustomModal/CustomModal";
import MasterDataForm from "~/src/components/pages/admin/masterdata/MasterData.Form";

interface IProps {
	data: MasterData[];
}

const MasterData: NextPage<IProps> = (props) => {
	const { data } = props;
	const modalRef = useRef<ModalRef>(null);
	const [dataSource, setDataSource] = useState<MasterData[]>(data)
	const topButtons: ButtonBaseProps[] = [
		{
			buttonName: "Tạo mới",
			onClick: () => onSave(),
			buttonType: "create",
		},
	];

	const onSave = (): void => {
		modalRef.current?.onOpen(<MasterDataForm onClose={() => modalRef?.current?.onClose()} />, "Tạo mới");
	};
	const onDelete = (): void => {};

	const getColumnConfig = () => {
		return [
			...MasterDataGridColumns,
			{
				title: "Hành động",
				width: 200,
				render: (value: any, record: any) => {
					console.log("value", value);
					console.log("record", record);
					return (
						<div className="flex items-center justify-center">
							<GridButtonBase type={"edit"} onClick={() => onSave()} />
							<GridButtonBase type={"delete"} onClick={() => onDelete()} />
						</div>
					);
				},
			},
		];
	};

	return (
		<div className="flex flex-col h-full w-full">
			<TopToolbar buttons={topButtons} />
			<Table
				columns={getColumnConfig()}
				dataSource={dataSource}
			/>
			<CustomModal ref={modalRef} />
		</div>
	);
};

export default MasterData;

export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	const headers = { Cookie: req.headers.cookie } as AxiosRequestHeaders;
	const response = (await ApiUtil.Axios.get(MASTER_DATA_INDEX_API, { headers })) as AxiosResponse<ApiResponse<MasterData[]>>;
	const fakeData = [
		{
			masterKey: "masterKey",
			code: "code",
			name: "name",
		},
	]
	return {
		props: {
			// data: response.data.result || [],
			data: fakeData,
		},
	};
};

// @ts-ignore
MasterData.getLayout = function getLayout(page: ReactElement) {
	return <AdminLayout>{page}</AdminLayout>;
};

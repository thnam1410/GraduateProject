import React, { ReactElement } from "react";
import { GetServerSideProps, NextPage } from "next";
import AdminLayout from "~/src/components/layout/AdminLayout";
import TopToolbar from "~/src/components/TopToolbar/TopToolbar";
import { ButtonProps } from "antd/lib/button/button";
import ButtonBase, { ButtonBaseProps } from "~/src/components/ButtonBase/ButtonBase";
import { ApiUtil } from "~/src/pages/utils/ApiUtil";
import { ApiResponse } from "~/src/pages/types/api.type";
import { MasterData } from "~/src/types/MasterData";
import { MASTER_DATA_INDEX_API } from "~/src/constants/apis/masterdata.api";
import { AxiosRequestHeaders, AxiosResponse } from "axios";
import { Table, Tooltip } from "antd";
import { MasterDataGridColumns } from "~/src/components/pages/admin/masterdata/masterdata.config";
import Icon, { CloseCircleOutlined, EditOutlined } from "@ant-design/icons";
import GridButtonBase from "~/src/components/ButtonBase/GridButtonBase";

interface IProps {
	data: MasterData[];
}

const MasterData: NextPage<IProps> = (props) => {
	const { data } = props;

	const topButtons: ButtonBaseProps[] = [
		{
			buttonName: "Tạo mới",
			onClick: () => onCreate(),
			buttonType: "create",
		},
	];

	const onCreate = (): void => {};
	const onEdit = (): void => {};
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
							<GridButtonBase type={"edit"} onClick={() => onEdit()} />
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
				dataSource={[
					{
						masterKey: "masterKey",
						code: "code",
						name: "name",
					},
				]}
			/>
		</div>
	);
};

export default MasterData;

export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	const headers = { Cookie: req.headers.cookie } as AxiosRequestHeaders;
	const response = (await ApiUtil.Axios.get(MASTER_DATA_INDEX_API, { headers })) as AxiosResponse<ApiResponse<MasterData[]>>;
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

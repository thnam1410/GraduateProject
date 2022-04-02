import React from "react";
import TextField from "~/src/components/FormFields/TextField";
import { Col, Form, Input, Row } from "antd";
import ButtonBase from "~/src/components/ButtonBase/ButtonBase";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { Controller } from "react-hook-form";
import { MasterData } from "~/src/types/MasterData";
import { ApiUtil } from "~/src/utils/ApiUtil";
import { MASTER_DATA_CREATE_API, MASTER_DATA_INDEX_API, MASTER_DATA_UPDATE_API } from "~/src/constants/apis/masterdata.api";
import { BaseForm } from "~/src/components/FormFields/BaseForm";

interface IProps {
	onClose: () => void;
	initData: IFormValues | null;
}
interface IFormValues {
	id?: string;
	masterKey: string;
	code: string;
	name: string;
}

const schema = yup
	.object({
		masterKey: yup.string().required("Vui lòng nhập dữ liệu!"),
		code: yup.string().required("Vui lòng nhập dữ liệu!"),
		name: yup.string().required("Vui lòng nhập dữ liệu!"),
	})
	.required();

const MasterDataForm: React.FC<IProps> = (props) => {
	const { initData, onClose } = props;
	const {
		formState: { errors },
	} = useForm<IFormValues>({
		resolver: yupResolver(schema),
		defaultValues: initData
			? {
					id: initData.id,
					code: initData.code,
					masterKey: initData.masterKey,
					name: initData.name,
			  }
			: {},
	});
	const onSubmit = async (formValues: IFormValues) => {
		const isUpdate = formValues.id ? true : false;
		const result = await ApiUtil.Axios.post(isUpdate ? MASTER_DATA_UPDATE_API : MASTER_DATA_CREATE_API, formValues);
		onClose();
	};
	return (
		<div>
			<BaseForm className="p-3 pb-2" onSubmit={onSubmit} resolver={yupResolver(schema)} defaultValues={props.initData as IFormValues}>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={"masterKey"} errors={errors} label={"Master Key"} />
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={"code"} errors={errors} label={"Mã"} />
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={"name"} errors={errors} label={"Tên"} />
						</Form.Item>
					</Col>
				</Row>
				<div className="footer flex justify-center items-center mt-4">
					<ButtonBase className="mr-2" buttonName={"Lưu"} buttonType="save" htmlType="submit" />
					<ButtonBase buttonName={"Đóng"} buttonType="close" onClick={onClose} />
				</div>
			</BaseForm>
		</div>
	);
};

export default MasterDataForm;

import React, { useRef } from "react";
import TextField from "~/src/components/FormFields/TextField";
import { Col, Form, Row } from "antd";
import ButtonBase from "~/src/components/ButtonBase/ButtonBase";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import BaseForm, { BaseFormRef } from "~/src/components/FormFields/BaseForm";
import { ApiUtil } from "~/src/utils/ApiUtil";
import { MASTER_DATA_CREATE_API, MASTER_DATA_UPDATE_API } from "~/src/constants/apis/masterdata.api";

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
	const formRef = useRef<BaseFormRef>(null);
	const onSubmit = async (formValues: IFormValues) => {
		const isUpdate = formValues.id ? true : false;
		const result = await ApiUtil.Axios.post(isUpdate ? MASTER_DATA_UPDATE_API : MASTER_DATA_CREATE_API, formValues);
		props.onClose();
	};
	return (
		<div>
			<BaseForm ref={formRef} className="p-3 pb-2" onSubmit={onSubmit} resolver={yupResolver(schema)} defaultValues={props.initData}>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={"masterKey"} label={"Master Key"} />
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={"code"} label={"Mã"} />
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={"name"} label={"Tên"} />
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

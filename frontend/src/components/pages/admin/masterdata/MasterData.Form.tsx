import React from "react";
import TextField from "~/src/components/FormFields/TextField";
import { Col, Form, Input, Row } from "antd";
import ButtonBase from "~/src/components/ButtonBase/ButtonBase";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { Controller } from "react-hook-form";
import { MasterData } from "~/src/types/MasterData";
import {BaseForm} from "~/src/components/FormFields/BaseForm";

interface IProps {
	onClose: () => void;
	initData: IFormValues | null;
}
interface IFormValues {
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
	const {
		formState: { errors },
	} = useForm<IFormValues>({
		resolver: yupResolver(schema),
		defaultValues: props.initData
			? {
					code: props.initData.code,
					masterKey: props.initData.masterKey,
					name: props.initData.name,
			  }
			: {},
	});
	const onSubmit = (formValues: IFormValues) => {
		console.log("IFormValues", formValues);
	};
	return (
		<div>
			<BaseForm className="p-3 pb-2" onSubmit={onSubmit} resolver={yupResolver(schema)} defaultValues={props.initData as IFormValues}>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={'masterKey'} errors={errors} label={"Master Key"} />
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={'code'} errors={errors} label={"Mã"} />

						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<TextField name={'name'} errors={errors} label={"Tên"} />
						</Form.Item>
					</Col>
				</Row>
				<div className="footer flex justify-center items-center mt-4">
					<ButtonBase className="mr-2" buttonName={"Lưu"} buttonType="save" htmlType="submit" />
					<ButtonBase buttonName={"Đóng"} buttonType="close" onClick={props.onClose} />
				</div>
			</BaseForm>
		</div>
	);
};

export default MasterDataForm;

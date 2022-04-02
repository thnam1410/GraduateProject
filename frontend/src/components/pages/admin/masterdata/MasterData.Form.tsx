import React from "react";
import TextField from "~/src/components/FormFields/TextField";
import { Col, Form, Input, Row } from "antd";
import ButtonBase from "~/src/components/ButtonBase/ButtonBase";
import { useForm } from "react-hook-form";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import { Controller } from "react-hook-form";
import { MasterData } from "~/src/types/MasterData";

interface IProps {
	onClose: () => void;
	initData: MasterData | null;
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
		control,
		handleSubmit,
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
			<Form className="p-3 pb-2" onFinish={handleSubmit(onSubmit)}>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<Controller
								name="masterKey"
								control={control}
								render={({ field }) => {
									console.log("field", field);
									return <TextField {...field} errors={errors} label={"Master Key"} />;
								}}
							/>
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<Controller
								name="code"
								control={control}
								render={({ field }) => <TextField {...field} errors={errors} label={"Mã"} />}
							/>
						</Form.Item>
					</Col>
				</Row>
				<Row className="m-2">
					<Col span={24}>
						<Form.Item>
							<Controller
								name="name"
								control={control}
								render={({ field }) => <TextField {...field} errors={errors} label={"Tên"} />}
							/>
						</Form.Item>
					</Col>
				</Row>
				<div className="footer flex justify-center items-center mt-4">
					<ButtonBase className="mr-2" buttonName={"Lưu"} buttonType="save" htmlType="submit" />
					<ButtonBase buttonName={"Đóng"} buttonType="close" onClick={props.onClose} />
				</div>
			</Form>
		</div>
	);
};

export default MasterDataForm;

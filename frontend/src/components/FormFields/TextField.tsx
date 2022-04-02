import React, { CSSProperties } from "react";
import { GetServerSideProps } from "next";
import { Input } from "antd";
import { InputProps } from "antd/lib/input/Input";
import { ErrorMessage } from "@hookform/error-message";
import { UseFormRegister } from "react-hook-form/dist/types/form";
import { Controller, useFormContext } from "react-hook-form";

interface IProps extends InputProps {
	label?: string;
	labelClass?: string;
	labelStyle?: CSSProperties;
	errors?: any;
	name: string;
}

const TextField: React.FC<IProps> = (props) => {
	const { labelClass, label, labelStyle = {}, name } = props;
	const methods = useFormContext();
	const { control, formState: {errors} } = methods;
	return (
		<div className="flex flex-col justify-start">
			<div className="flex items-center">
				{label && (
					<label
						className={`mr-2 font-bold ${labelClass}`}
						style={{
							width: 100,
							...labelStyle,
						}}
					>
						{label}
					</label>
				)}
				<Controller
					name={name}
					control={control}
					render={({ field }) => <Input {...field} {...props} placeholder={label + "..." || ""} />}
				/>
			</div>
			{errors && (
				<ErrorMessage
					errors={errors}
					name={name}
					render={({ message }) => (message ? <p className="m-0 text-red-400">{message}</p> : null)}
				/>
			)}
		</div>
	);
};

export default TextField;

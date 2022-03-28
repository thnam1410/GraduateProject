import React, { CSSProperties } from "react";
import { Button } from "antd";
import { ButtonProps } from "antd/lib/button/button";

export interface ButtonBaseProps extends ButtonProps {
	buttonName: string | JSX.Element;
	buttonType?: "create" | "edit" | "detail" | "delete";
}

const ButtonBase: React.FC<ButtonBaseProps> = (props) => {
	const { style, buttonType } = props;

	const getButtonTypeStyle = (): [ButtonProps, CSSProperties] => {
		let buttonTypeStyle: ButtonProps = {};
		let overrideStyle: CSSProperties = {};
		switch (buttonType) {
			case "create":
				buttonTypeStyle = { type: "primary" };
				break;
			case "detail":
				overrideStyle = { backgroundColor: "#7518c4", color: "white" };
				break;
			case "edit":
				overrideStyle = { backgroundColor: "#46ad50", color: "white" };
				break;
			case "delete":
				buttonTypeStyle = { danger: true };
				break;
			default:
				break;
		}
		return [buttonTypeStyle, overrideStyle];
	};

	const [buttonTypeStyle, overrideStyle] = getButtonTypeStyle();
	const buttonBaseStyle: CSSProperties = {
		borderRadius: 3,
		boxShadow: "box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;",
	};
	const buttonStyle = Object.assign(buttonBaseStyle, style || {});
	return (
		<Button {...props} {...buttonTypeStyle} style={Object.assign(buttonStyle, overrideStyle || {})}>
			{props.buttonName}
		</Button>
	);
};

export default ButtonBase;

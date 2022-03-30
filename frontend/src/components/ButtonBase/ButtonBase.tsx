import React, { CSSProperties } from "react";
import { Button } from "antd";
import { ButtonProps } from "antd/lib/button/button";
import styles from "./ButtonBase.module.scss";

export interface ButtonBaseProps extends ButtonProps {
	buttonName: string | JSX.Element;
	buttonType?: "create" | "edit" | "detail" | "delete" | "save" | "close";
	className?: string;
	// errors?: <Record
}

const ButtonBase: React.FC<ButtonBaseProps> = (props) => {
	const { style, buttonType, className } = props;
	const getButtonTypeStyle = (): [ButtonProps, CSSProperties, string] => {
		let buttonTypeStyle: ButtonProps = {};
		let overrideStyle: CSSProperties = {};
		let className = "";
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
			case "save":
				className = "btn-save";
				overrideStyle = { backgroundColor: "#40a9ff", color: "white" };
				break;
			case "close":
				className = "btn-close";
				overrideStyle = { backgroundColor: "grey", color: "white" };
				break;
			default:
				break;
		}
		return [buttonTypeStyle, overrideStyle, className];
	};

	const [buttonTypeStyle, overrideStyle, btnClass] = getButtonTypeStyle();
	const buttonBaseStyle: CSSProperties = {
		borderRadius: 3,
		boxShadow: "box-shadow: rgba(99, 99, 99, 0.2) 0px 2px 8px 0px;",
		fontWeight: "bold",
	};
	const buttonStyle = Object.assign(buttonBaseStyle, style || {});
	return (
		<div className={`${styles["button-base"]} ${className}`}>
			<Button className={btnClass} {...props} {...buttonTypeStyle} style={Object.assign(buttonStyle, overrideStyle || {})}>
				{props.buttonName}
			</Button>
		</div>
	);
};

export default ButtonBase;

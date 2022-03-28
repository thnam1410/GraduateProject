import React from "react";
import { ButtonProps } from "antd/lib/button/button";
import ButtonBase from "~/src/components/ButtonBase/ButtonBase";

interface IProps {
	buttons: ButtonProps[];
}

const TopToolbar: React.FC<IProps> = ({ buttons = [] }) => {
	return (
		<div className="flex justify-end items-center p-2">
			{buttons.map((buttonProps, key) => {
				return <ButtonBase key={key} {...buttonProps} />;
			})}
		</div>
	);
};

export default TopToolbar;

import React from "react";
import { CloseCircleOutlined, EditOutlined, ProfileOutlined } from "@ant-design/icons";
import { Tooltip } from "antd";

interface IProps {
	type: "detail" | "edit" | "delete";
	onClick: () => void;
}

const GridButtonBase: React.FC<IProps> = (props) => {
	const { type, onClick } = props;
	const getButtonProperties = () => {
		let body = null;
		let title = "";
		switch (type) {
			case "detail":
				title = "Chi tiết";
				body = (
					<button
						type="button"
						className="text-white bg-gradient-to-r from-purple-500 via-purple-600 to-purple-700 hover:bg-gradient-to-br focus:outline-none focus:ring-purple-300 dark:focus:ring-purple-800 font-medium rounded-lg text-sm px-4 py-2 text-center mr-2 mb-2"
					>
						<ProfileOutlined size={18} />
					</button>
				);
				break;
			case "edit":
				title = "Sửa";
				body = (
					<button
						type="button"
						className="text-white bg-gradient-to-r from-green-400 via-green-500 to-green-600 hover:bg-gradient-to-br  focus:outline-none focus:ring-green-300 dark:focus:ring-green-800 font-medium rounded-lg text-sm px-4 py-2 text-center mr-2 mb-2"
						onClick={onClick}
					>
						<EditOutlined size={18} />
					</button>
				);
				break;
			case "delete":
				title = "Xóa";
				body = (
					<button
						type="button"
						className="text-white bg-gradient-to-r from-red-400 via-red-500 to-red-600 hover:bg-gradient-to-br  focus:outline-none focus:ring-red-300 dark:focus:ring-red-800 font-medium rounded-lg text-sm px-4 py-2 text-center mr-2 mb-2"
					>
						<CloseCircleOutlined size={18} />
					</button>
				);
				break;
		}
		return { body, title };
	};
	const { body, title } = getButtonProperties();
	return <Tooltip title={title}>{body}</Tooltip>;
};

export default GridButtonBase;

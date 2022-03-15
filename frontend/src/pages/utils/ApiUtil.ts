import axios from "axios";
import { notification } from "antd";
import { ArgsProps } from "antd/lib/notification";
import * as https from "https";
import 'antd/dist/antd.css';

const AxiosClient = axios.create({
	baseURL: process.env.NEXT_PUBLIC_API_URL,
	withCredentials: true,
	httpsAgent: new https.Agent({
		rejectUnauthorized: false
	})
});
export const BASE_API_PATH = "/api";

export class ApiUtil {
	static Axios = AxiosClient;

	static ToastSuccess = (message: string, title?: string, configs?: ArgsProps): void => {
		notification.success({
			message: title || "Thông báo",
			description: message || "Có thông báo!",
			// className: "notification-success",
			style: getNotificationStyle("success"),
		});
	};
	static ToastError = (message: string, title?: string, configs?: ArgsProps): void => {
		notification.error({
			message: title || "Thông báo",
			description: message || "Có thông báo!",
			// className: "notification-error",
			style: getNotificationStyle("error"),
		});
	};
}

const getNotificationStyle = (type: "success" | "warning" | "error" | "info") => {
	return {
		success: {
			color: "rgba(0, 0, 0, 0.65)",
			border: "1px solid #b7eb8f",
			backgroundColor: "#f6ffed",
		},
		warning: {
			color: "rgba(0, 0, 0, 0.65)",
			border: "1px solid #ffe58f",
			backgroundColor: "#fffbe6",
		},
		error: {
			color: "rgba(0, 0, 0, 0.65)",
			border: "1px solid #ffa39e",
			backgroundColor: "#fff1f0",
		},
		info: {
			color: "rgba(0, 0, 0, 0.65)",
			border: "1px solid #91d5ff",
			backgroundColor: "#e6f7ff",
		},
	}[type];
};
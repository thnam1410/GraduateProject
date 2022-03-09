import { notification } from "antd";
import { ArgsProps } from "antd/lib/notification";

export default class NotifyUtils {
	static ToastSuccess = (message: string, title?: string, configs?: ArgsProps): void => {
		notification.success({
			message: title || "Thông báo",
			description: message || "Có thông báo!",
			className: "notification-success",
		});
	};
	static ToastError = (message: string, title?: string, configs?: ArgsProps): void => {
		notification.error({
			message: title || "Thông báo",
			description: message || "Có thông báo!",
			className: "notification-error",
		});
	};
}

import React, { forwardRef, ReactNode, useImperativeHandle, useState } from "react";
import { Spin } from "antd";
export type OverlayRef = {
	open: (text?: ReactNode) => void;
	close: () => void;
};
interface IState {
	text: ReactNode;
	open: boolean;
}
const Overlay = forwardRef((props, ref) => {
	const [state, setState] = useState<IState>({
		text: (
			<span>
				Đang xử lý....
				<br />
				Vui lòng chờ trông giây lát.
			</span>
		),
		open: false,
	});

	const open = (text?: ReactNode) => {
		if (text) {
			setState({
				open: true,
				text,
			});
		} else {
			setState((prev) => ({ ...prev, open: true }));
		}
	};

	const close = () => {
		setState({
			open: false,
			text: "",
		});
	};

	useImperativeHandle(
		ref,
		() => ({
			open,
			close,
		}),
		[]
	);

	if(!state.open) return null;
	return (
		<div
			style={{
				position: "absolute",
				top: 0,
				right: 0,
				left: 0,
				bottom: 0,
				background: "rgba(255,255,255,0.5)",
				display: "flex",
				alignItems: "center",
				justifyContent: "center",
			}}
		>
			<div
				style={{
					zIndex: 99999,
				}}
			>
				<div className="loading">
					<Spin tip={state.text} />
				</div>
			</div>
		</div>
	);
});
Overlay.displayName = "Overlay";
export default Overlay;

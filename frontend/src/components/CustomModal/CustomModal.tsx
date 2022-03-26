import { Modal } from "antd";
import React, { forwardRef, useImperativeHandle } from "react";
import useMergeState from "~/src/hooks/useMergeState";

export type ModalRef = {
	onClose: () => void;
	onOpen: (Component: JSX.Element, title: string | React.ElementType) => void;
};
interface IState {
	visible: boolean;
	title: string | React.ElementType;
	children: JSX.Element | null;
}
const CustomModal = forwardRef((props, ref) => {
	const [state, setState] = useMergeState<IState>({
		visible: false,
		title: "",
		children: null,
	});

	React.useEffect(() => {
		return () => {
			console.log("unmount");
		};
	}, []);

	const onOpen = (Component: JSX.Element, title: string | React.ElementType) => {
		setState({
			title,
			visible: true,
			children: Component,
		});
	};

	const onClose = () => {
		setState({ visible: false });
	};

	useImperativeHandle(
		ref,
		() => ({
			onClose,
			onOpen,
		}),
		[onClose, onOpen]
	);

	const handleOk = () => setState({ visible: true });
	const handleCancel = () => setState({ visible: false });

	if (!state.visible) return null;
	return (
		<Modal visible={state.visible} title={state.title} onOk={handleOk} onCancel={handleCancel}>
			{state.children}
		</Modal>
	);
});

CustomModal.displayName = "CustomModal";
export default CustomModal;

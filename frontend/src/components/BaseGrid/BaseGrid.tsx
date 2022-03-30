import React from "react";
import { AgGridReact, AgGridReactProps, AgReactUiProps } from "ag-grid-react";

interface IProps extends AgGridReactProps, AgReactUiProps {}

const BaseGrid: React.FC<IProps> = (props) => {
	return (
		<div className="h-full w-full">
			<AgGridReact {...props} className="ag-theme-alpine" />
		</div>
	);
};

export default BaseGrid;

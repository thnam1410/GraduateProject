import React from "react";
import { AgGridReact, AgGridReactProps, AgReactUiProps } from "ag-grid-react";

interface IProps extends AgGridReactProps, AgReactUiProps {}

const BaseGrid: React.FC<IProps> = (props) => {
	const defaultColDef = {
		editable: true,
		enableRowGroup: true,
		enablePivot: true,
		enableValue: true,
		sortable: true,
		resizable: true,
		filter: true,
		flex: 1,
		minWidth: 100,
	};
	return (
		<div className="h-full w-full">
			<AgGridReact {...props} className="ag-theme-alpine" defaultColDef={defaultColDef} />
		</div>
	);
};

export default BaseGrid;

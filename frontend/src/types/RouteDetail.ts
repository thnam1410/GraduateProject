export type RouteDetail = {
	routeVarName: string;
	routeNo: string;
	distance: number;
	endStop?: string;
	outbound?: boolean;
	routeVarShortName?: string;
	runningTime?: number;
	startStop?: string;
	routeId: number;
};

export type RouteInfoSearchView = {
	date: string;
	infoRouteSearchList: RouteInfoSearch[];
};

export type RouteInfoSearch = {
	userId: string | null;
	routeId: number | null;
	isSearch: boolean | null;
	departPoint: string | null;
	destination: string | null;
	timeSearch: string | null;
	routeInfo: RouteInfo | null;
};

export type RouteInfo = {
	name: string;
	routeCode: string;
	type: string;
	routeRange: string;
	busType: string;
	timeRange: string;
	unit: string;
};

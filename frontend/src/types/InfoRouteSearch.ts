export type RouteInfoSearchView = {
	date: string;
	infoRouteSearchList: RouteInfoSearch[];
};

export type RouteInfoSearch = {
	isSearch: boolean;
	departPoint: string;
	destination: string;
	timeSearch: string;
	routeInfo: RouteInfo;
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

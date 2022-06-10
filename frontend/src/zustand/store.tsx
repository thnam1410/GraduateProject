import create from "zustand";
import _ from "lodash";
import { LatLngTuple } from "leaflet";
import { Position } from "~/src/types/Common";
import { UserSession } from "../types/UserInfo";

export const useStore = create<MapStore>((set) => ({
	positions: [],
	setPositions: (positions) => {
		set((state) => ({ ...state, positions }));
	},
	userSession: null,
	infoRouteSearch: null,
	setInfoRouteSearch: (infoRouteSearch) => {
		set((state) => ({ ...state, infoRouteSearch }));
	},
	setUserSession: (userSession) => {
		set((state) => ({ ...state, userSession }));
	},
	positionZoomIn: [],
	setPositionZoomIn: (positionZoomIn) => {
		set((state) => ({ ...state, positionZoomIn }));
	},
	isOpen:false,

	positionsBusStop: [],
	setIsOpen :(isOpen) => {
	set((state) => ({ ...state, isOpen }));
	}, 
	setPositionsBusStop: (positionsBusStop) => {
		set((state) => ({ ...state, positionsBusStop }));
	},
	setPositionAndBusStop: (positions, positionsBusStop) => {
		set((state) => ({ ...state, positions, positionsBusStop }));
	},
	isAllList: true,
	infoRouteDetail: null,
	setStateRouteInfoView: (payload) => {
		set((state) => ({
			...state,
			isAllList: payload.isAllList,
			infoRouteDetail: payload.infoRouteDetail,
			positions: payload.positions,
			positionZoomIn: payload.positionZoomIn,
			positionsBusStop: payload.positionsBusStop,
		}));
	},
	setStateRouteActionBackInfoView: (payload) => {
		set((state) => ({ ...state, isAllList: payload.isAllList, infoRouteDetail: null, positions: [], positionsBusStop: [] }));
	},
}));

interface MapStore {
	positions: LatLngTuple[];
	positionZoomIn: LatLngTuple[];
	isAllList: boolean;
	infoRouteDetail: RouteDetailInfo | null;
	positionsBusStop: CustomBusStopLatLngTuble[];
	userSession: UserSession | null;
	isOpen: boolean | null;
	infoRouteSearch:RouteInfoSearchView | null;
	setInfoRouteSearch: (value : RouteInfoSearchView) => void;
	setIsOpen: (value: boolean) => void;
	setUserSession: (value: UserSession) => void;
	setPositions: (value: LatLngTuple[]) => void;
	setPositionZoomIn: (positionZoomIn: LatLngTuple[]) => void;
	setPositionsBusStop: (positionsBusStop: CustomBusStopLatLngTuble[]) => void;
	setPositionAndBusStop: (positions: any, positionsBusStop: CustomBusStopLatLngTuble[]) => void;
	setStateRouteInfoView: (value: {
		isAllList: boolean;
		infoRouteDetail: any;
		positions: LatLngTuple[];
		positionZoomIn: LatLngTuple[];
		positionsBusStop: CustomBusStopLatLngTuble[];
	}) => void;
	setStateRouteActionBackInfoView: (value: any) => void;
}

export type CustomBusStopLatLngTuble = {
	pos: [number, number];
	name?: string;
};

export type RouteDetailInfo = {
	routeInfo: RouteInfo;
	forwardRouteStops: { name: string; position: Position }[];
	forwardRoutePos: Position[];
	backwardRouteStops: { name: string; position: Position }[];
	backwardRoutePos: Position[];
};

export type RouteInfoSearchView = {
	date : string;
	infoRouteSearchList: RouteInfoSearch[];
}

export type RouteInfoSearch = {
	isSearch : boolean;
	departPoint :string;
	destination:string;
	timeSearch : string;
	routeInfo: RouteInfo;
}

export type RouteInfo = {
	name: string;
	routeCode: string;
	type: string;
	routeRange: string;
	busType: string;
	timeRange: string;
	unit: string;
}
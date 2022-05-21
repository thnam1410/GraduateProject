import create from "zustand";
import _ from "lodash";
import { LatLngTuple } from "leaflet";
import { Position } from "~/src/types/Common";

export const useStore = create<MapStore>((set) => ({
	positions: [],
	setPositions: (positions) => {
		set((state) => ({ ...state, positions }));
	},
	positionZoomIn : [],
	setPositionZoomIn: (positionZoomIn) => {
		set((state) => ({ ...state, positionZoomIn }));
	},

	positionsBusStop: [],
	setPositionsBusStop: (positionsBusStop) => {
		set((state) => ({ ...state, positionsBusStop }));
	},
	setPositionAndBusStop: (positions, positionsBusStop) => {
		set((state) => ({ ...state, positions, positionsBusStop }));
	},
	//RouteInfoView
	isAllList: true,
	infoRouteDetail: null,
	setStateRouteInfoView: (payload) => {
		set((state) => ({
			...state,
			isAllList: payload.isAllList,
			infoRouteDetail: payload.infoRouteDetail,
			positions: payload.positions,
			positionZoomIn : payload.positionZoomIn,
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
	routeInfo: {
		name: string;
		routeCode: string;
		type: string;
		routeRange: string;
		busType: string;
		timeRange: string;
		unit: string;
	};
	forwardRouteStops: { name: string; position: Position }[];
	forwardRoutePos: Position[];
	backwardRouteStops: { name: string; position: Position }[];
	backwardRoutePos: Position[];
};

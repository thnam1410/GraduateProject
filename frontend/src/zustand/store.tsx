import create from "zustand";
import _ from "lodash";
import { LatLngTuple } from "leaflet";
import { Position } from "~/src/types/Common";

export const useStore = create<MapStore>((set) => ({
	positions: [],
	setPositions: (positions) => {
		console.log("setPositions", positions);
		set((state) => ({ ...state, positions }));
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
			positionsBusStop: payload.positionsBusStop,
		}));
	},
	setStateRouteActionBackInfoView: (payload) => {
		console.log("setStateRouteActionBackInfoView");
		set((state) => ({ ...state, isAllList: payload.isAllList, infoRouteDetail: null, positions: [] }));
	},
}));

interface MapStore {
	positions: LatLngTuple[];
	isAllList: boolean;
	infoRouteDetail: RouteDetailInfo | null;
	positionsBusStop: CustomBusStopLatLngTuble[];
	setPositions: (value: LatLngTuple[]) => void;
	setPositionsBusStop: (positionsBusStop: CustomBusStopLatLngTuble[]) => void;
	setPositionAndBusStop: (positions: any, positionsBusStop: CustomBusStopLatLngTuble[]) => void;
	setStateRouteInfoView: (value: {
		isAllList: boolean;
		infoRouteDetail: any;
		positions: LatLngTuple[];
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

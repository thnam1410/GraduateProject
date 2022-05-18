import create from "zustand";
import _ from "lodash";
import { LatLngTuple } from "leaflet";
import { Position } from "~/src/types/Common";

export const useStore = create<MapStore>((set) => ({
	positions: [],
	setPositions: (positions) => {
		console.log("setPositions");
		set((state) => ({ ...state, positions }));
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

	setPositions: (value: LatLngTuple[]) => void;
	setStateRouteInfoView: (value: { isAllList: boolean; infoRouteDetail: any; positions: LatLngTuple[] }) => void;
	setStateRouteActionBackInfoView: (value: any) => void;
}

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
	forwardRouteStops: { name: string; position: Position[] };
	forwardRoutePos: Position[];
	backwardRouteStops: { name: string; position: Position[] };
	backwardRoutePos: Position[];
};

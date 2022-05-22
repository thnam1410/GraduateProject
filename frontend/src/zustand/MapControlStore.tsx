import create from "zustand";
import {LatLngTuple} from "leaflet";
import {RouteDetail} from "~/src/types/RouteDetail";

export type RoutePathDetailDto = {
	isSwitch: boolean;
	positions: LatLngTuple[];
};

export type RoutePathDto = {
	paths: Record<number, RoutePathDetailDto>;
	routeDetailList: RouteDetail[];
}

interface MapControlStore {
	isFindPathMap: boolean;
	routePaths: RoutePathDto | null;
	switchMap: () => void;
	setRoutePath: (routePaths: RoutePathDto) => void;
}

export const useMapControlStore = create<MapControlStore>((set, get) => ({
	isFindPathMap: false,
	routePaths: null,
	switchMap: () =>
		set((state) => ({
			...state,
			isFindPathMap: !state.isFindPathMap,
		})),
	setRoutePath: (routePaths) =>
		set((state) => ({
			...state,
			routePaths,
		})),
}));

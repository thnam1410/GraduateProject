import create from "zustand";
import { LatLngTuple } from "leaflet";

export type RoutePathDetailDto = {
	isSwitch: boolean;
	positions: LatLngTuple[];
};

export type RoutePathDto = Record<number, RoutePathDetailDto>;

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

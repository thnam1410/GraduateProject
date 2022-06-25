import create from "zustand";
import { LatLngTuple } from "leaflet";
import { StopDto } from "~/src/types/Stop";
import {Position} from '~/src/types/Common';

export type MapControlStoreV2 = {
	positions: Position[];
	stops: StopDto[];
	isFindPathMap: boolean;
	switchMap: () => void;
	setPath: (data: Pick<MapControlStoreV2, 'positions' | 'stops'>) => void;
};

export const useMapControlStoreV2 = create<MapControlStoreV2>((set, get) => ({
	positions: [],
	stops: [],
	isFindPathMap: false,
	switchMap: () =>
		set((state) => ({
			...state,
			isFindPathMap: !state.isFindPathMap,
		})),
	setPath: (data) =>
		set((state) => ({
			...state,
			...data
		})),
}));

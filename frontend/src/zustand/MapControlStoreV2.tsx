import create from "zustand";
import { LatLngTuple } from "leaflet";
import { StopDto } from "~/src/types/Stop";
import { Position } from "~/src/types/Common";

export type Destination = {
	position: Position;
	address: string;
	title: string;
};

export type MapControlStoreV2 = {
	positions: Position[];
	stops: StopDto[];
	destination: Destination[];
	isFindPathMap: boolean;
	switchMap: () => void;
	setPath: (data: Pick<MapControlStoreV2, "positions" | "stops">, destinationData: Destination[]) => void;
};

export const useMapControlStoreV2 = create<MapControlStoreV2>((set, get) => ({
	isFindPathMap: false,
	positions: [],
	stops: [],
	destination: [],
	switchMap: () =>
		set((state) => ({
			...state,
			isFindPathMap: !state.isFindPathMap,
		})),
	setPath: (pathData, destinationData) =>
		set((state) => ({
			...state,
			...pathData,
			destination: destinationData,
		})),
}));

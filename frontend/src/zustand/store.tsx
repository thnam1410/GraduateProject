import create from "zustand";
import _ from "lodash";
export const useStore = create((set) => ({
	positions: [],
	setPositions: (value: any[]) => {
		set((state) => ({ positions: value }));
	},
	//RouteInfoView
	isAllList: true,
	infoRouteDetail: null,
	setStateRouteInfoView: (value: any) => {
		set((state) => ({ isAllList: value.isAllList, infoRouteDetail: value.infoRouteDetail }));
	},
	setStateRouteActionBackInfoView: (value: any) => {
		set((state) => ({ isAllList: value.isAllList, infoRouteDetail: [], positions: [] }));
	},
}));

import create from "zustand";
import _ from "lodash";
export const useStore = create((set) => ({
	positions: [],
	setPositions: (value: any[]) => {
		const dataPositions = _.map(value, (item) => ({
			lat: item?.position?.lat,
			lng: item?.position?.lng,
		}));
		set((state) => ({ positions: dataPositions }));
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

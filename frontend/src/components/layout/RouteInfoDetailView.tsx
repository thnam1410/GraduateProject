import { NextPage } from "next";
import { useState } from "react";
import { useStore } from "~/src/zustand/store";
import { PathIconBack } from "../pages/svg/Path";
import InfoDetailTabView from "./InfoDetailTabView";

const RouteInfoDetailView: NextPage<any> = () => {
	const [openTab, setOpenTab] = useState<number>(1);
	const infoRouteDetail = useStore((state) => state.infoRouteDetail);
	const setStateRouteActionBackInfoView = useStore((state) => state.setStateRouteActionBackInfoView);
	const setPositions = useStore((state) => state.setPositions);
	const setPositionAndBusStop = useStore((state) => state.setPositionAndBusStop);
	const { backwardRouteStops, forwardRouteStops, backwardRoutePos, forwardRoutePos } = infoRouteDetail || {};
	return (
		<>
			<div className="flex mt-6 ml-2 h-12">
				<div
					className="cursor-pointer mb-2 border-gray-400"
					onClick={() => {
						setStateRouteActionBackInfoView({ isAllList: true });
					}}
				>
					<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384.965 384.965" className="w-3 h-4 mx-1 sm:w-16 sm:h-5">
						<path d={PathIconBack} />
					</svg>
				</div>
				<div className="h-3">
					<p className="text-2xl leading-6 font-medium">Tuyến số {infoRouteDetail?.routeInfo?.routeCode}</p>
				</div>
			</div>

			<div className="h-full w-full">
				<ul className="flex mb-0 list-none flex-wrap pt-3 pb-4 flex-row" role="tablist">
					<li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 1 ? "text-white bg-blue-600" : "text-blue-600 bg-white")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(1);
								if (infoRouteDetail?.forwardRouteStops) {
									// setPositions(infoRouteDetail.forwardRoutePos.map((x) => [x.lat, x.lng]));
									const positions = infoRouteDetail.forwardRoutePos.map((x) => [x.lat, x.lng]);
									const positionBusStop = infoRouteDetail.forwardRouteStops.map((x) => ({
										name: x.name,
										pos: [x.position.lat, x.position.lng] as [number, number],
									}));
									setPositionAndBusStop(positions, positionBusStop);
								}
							}}
							data-toggle="tab"
							href="#link1"
							role="tablist"
						>
							Xem lượt đi
						</a>
					</li>
					<li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 2 ? "text-white bg-blue-600" : "text-blue-600 bg-white")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(2);
								if (infoRouteDetail?.backwardRoutePos) {
									const positions = infoRouteDetail.backwardRoutePos.map((x) => [x.lat, x.lng]);
									const positionBusStop = infoRouteDetail.backwardRouteStops.map((x) => ({
										name: x.name,
										pos: [x.position.lat, x.position.lng] as [number, number],
									}));
									setPositionAndBusStop(positions, positionBusStop);
								}
							}}
							data-toggle="tab"
							href="#link2"
							role="tablist"
						>
							Xem lượt về
						</a>
					</li>
				</ul>
				<div className=" relative w-full flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
					<div className="w-full   flex-auto">
						<div className="w-full tab-content">
							{(() => {
								switch (openTab) {
									case 1:
										return (
											<div id="link1">
												<InfoDetailTabView
													data={infoRouteDetail}
													routePos={backwardRoutePos}
													routeStopList={forwardRouteStops}
												/>
											</div>
										);
									case 2:
										return (
											<div id="link2">
												<InfoDetailTabView
													data={infoRouteDetail}
													routePos={forwardRoutePos}
													routeStopList={backwardRouteStops}
												/>
											</div>
										);
								}
							})()}
						</div>
					</div>
				</div>
			</div>
		</>
	);
};

export default RouteInfoDetailView;

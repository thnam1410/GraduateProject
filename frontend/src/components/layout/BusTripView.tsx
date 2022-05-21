import { NextPage } from "next";
import { useState } from "react";
import { useStore } from "~/src/zustand/store";
import { PathIconBack } from "../pages/svg/Path";
import InfoDetailTabView from "./InfoDetailTabView";

const BusTripView: NextPage<any> = () => {
	const [openTab, setOpenTab] = useState<number>(1);
	const infoRouteDetail = useStore((state) => state.infoRouteDetail);
	const setStateRouteActionBackInfoView = useStore((state) => state.setStateRouteActionBackInfoView);
	const setPositions = useStore((state) => state.setPositions);
	const setPositionAndBusStop = useStore((state) => state.setPositionAndBusStop);
	const { backwardRouteStops, forwardRouteStops, backwardRoutePos, forwardRoutePos } = infoRouteDetail || {};


    const renderBuses = ()  =>{
        return (
            <>
                     <li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 1 ? "text-white bg-blue-600" : "text-blue-600 bg-white")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(1);
							}}
							data-toggle="tab"
							href="#link1"
							role="tablist"
						>
							1 Tuyến
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
							}}
							data-toggle="tab"
							href="#link1"
							role="tablist"
						>
							2 Tuyến
						</a>
					</li>
                    <li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 3 ? "text-white bg-blue-600" : "text-blue-600 bg-white")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(3);
							}}
							data-toggle="tab"
							href="#link1"
							role="tablist"
						>
							3 Tuyến
						</a>
					</li>
            </>
        )
    }

	return (
		<>
			<div className="flex mt-6 ml-2 h-12">
				<div className="h-3">
					<p className="text-2xl leading-6 font-medium">SỐ TUYẾN ĐI TỐI ĐA</p>
				</div>
			</div>
			<div className="h-screen w-full">
				<ul className="flex mb-0 list-none flex-wrap pt-3 pb-4 flex-row" role="tablist">
					{renderBuses()}
				</ul>
				<div className=" relative w-full flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
					<div className="w-full   flex-auto">
						<div className="w-full tab-content">
							{(() => {
								switch (openTab) {
									case 1:
										return (
											<div id="link1">
												aaaaa
											</div>
										);
									case 2:
										return (
											<div id="link2">
												bbbbb
											</div>
										);
                                    case 3:
										return (
											<div id="link2">
												cccc
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

export default BusTripView;

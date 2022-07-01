import { NextPage } from "next";
import { useState } from "react";
import { useStore } from "~/src/zustand/store";
import { PathIconBack, pathIconBus_1, pathIconBus_2 } from "../pages/svg/Path";
import InfoDetailTabView from "./InfoDetailTabView";
import { useMapControlStore } from "~/src/zustand/MapControlStore";
import {useMapControlStoreV2} from '~/src/zustand/MapControlStoreV2';

const BusTripView: NextPage<any> = () => {
	const stops = useMapControlStoreV2((state) => state.stops);
	return (
		<div className="h-full w-full mt-3">
			<div className=" relative w-full flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
				<div className="w-full flex-auto">
					<div className="w-full tab-content">
						<div className="-mb-px last:mr-0 flex-auto text-center">
							<div
								className={
									"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal text-white bg-blue-600"
								}
								data-toggle="tab"
							>
								Lộ trình
							</div>
						</div>
						<div className="guide-paths mt-3" style={{maxHeight: 550, overflowY: "scroll"}}>
							{(stops || []).map((stop) => {
								console.log('stop',stop)
								return (
									<div className="guide-item flex px-2 py-1 rounded border-gray-400" key={stop.name + stop.addressNo}>
										<div className={"icon"} style={{width: 65}}>
											{" "}
											<div className="bg-blue-light shadow-border p-3 w-4 h-4">
												<svg
													xmlns="http://www.w3.org/2000/svg"
													className="w-4 h-4 sm:w-6 sm:h-6 mr-2"
													viewBox="0 0 472.666 472.666"
												>
													<path d={pathIconBus_1} />
													<path d={pathIconBus_2} />
												</svg>
											</div>
										</div>
										<div className={"flex-1"}>
											<h3 className='font-bold'>{stop.name}</h3>
											<h4 className='font-bold'>Mã Tuyến: {stop.routes}</h4>
											<p>Địa chỉ: {stop.addressNo}</p>
										</div>
									</div>
								);
							})}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};

export default BusTripView;

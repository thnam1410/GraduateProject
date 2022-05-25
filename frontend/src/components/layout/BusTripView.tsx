import { NextPage } from "next";
import { useState } from "react";
import { useStore } from "~/src/zustand/store";
import { PathIconBack } from "../pages/svg/Path";
import InfoDetailTabView from "./InfoDetailTabView";
import { useMapControlStore } from "~/src/zustand/MapControlStore";

const BusTripView: NextPage<any> = () => {
	const routePaths = useMapControlStore((state) => state.routePaths);
	console.log("routePaths", routePaths);
	return (
		<div className="h-screen w-full mt-3">
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
					</div>
				</div>
			</div>
		</div>
	);
};

export default BusTripView;

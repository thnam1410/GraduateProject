import { NextPage } from "next";
import { useState } from "react";
import { PathIconBack } from "../pages/svg/Path";

const RouteInfoDetailView: NextPage<any> = (props) => {
	const [openTab, setOpenTab] = useState<number>(1);

	return (
		<>
			<div className="flex mt-6 ml-2 h-12">
				<div
					className="cursor-pointer mb-2 border-gray-400"
					onClick={() => {
						props.handleOnChangeBack();
					}}
				>
					<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384.965 384.965" className="w-3 h-4 mx-1 sm:w-16 sm:h-5">
						<path d={PathIconBack} />
					</svg>
				</div>
				<div className="h-3">
					<p className="text-2xl leading-6 font-medium">Tuyến số {props.data?.routeInfo?.routeCode}</p>
				</div>
			</div>

			<div className="h-screen w-full">
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
							}}
							data-toggle="tab"
							href="#link2"
							role="tablist"
						>
							Xem lượt về
						</a>
					</li>
				</ul>
				<div className=" relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
					<div className=" px-4 py-5 flex-auto">
						<div className=" tab-content tab-space">
							<div className={openTab === 1 ? "block " : "hidden"} id="link1">
								{/* <RouteLookupListView handleOnChangeDiv={handleOnChangeDiv} /> */}b
							</div>
							<div className={openTab === 2 ? "block" : "hidden"} id="link2">
								<p>a</p>
							</div>
						</div>
					</div>
				</div>
			</div>
		</>
	);
};

export default RouteInfoDetailView;

import { GetServerSideProps, NextPage } from "next";
import { useEffect, useState } from "react";
import { useStore } from "~/src/zustand/store";
import RouteDescriptionView from "./RouteDescriptionView";
import RouteStopList from "./RouteStopList";




const InfoDetailTabView: NextPage<any> = (props) => {
	const [openTab, setOpenTab] = useState<number>(1);
	const routeStopList = props.routeStopList;
	const routePos = props.routePos;
	const data = props.data;

	return (
		<>
			<div className="h-screen w-full">
				<ul className="flex mb-0 list-none flex-wrap pt-3 pb-4 flex-row" role="tablist">
					<li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 1 ? "text-gray bg-gray-600" : "text-white-600 bg-gray")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(1);
							}}
							data-toggle="tab"
							href="#link2"
							role="tablist"
						>
							Trạm dừng
						</a>
					</li>
					<li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 2 ? "text-gray bg-gray-600" : "text-white-600 bg-gray")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(2);
							}}
							data-toggle="tab"
							href="#link2"
							role="tablist"
						>
							Thông tin
						</a>
					</li>
				</ul>
				<div className=" relative w-full flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
					<div className="w-full  px-4 py-5 flex-auto">
						<div className="w-full tab-content tab-space">
							<div className={openTab === 1 ? "block" : "hidden"} id="link2">
								<RouteStopList routeStopList={routeStopList} />
							</div>
							<div className={openTab === 2 ? "block" : "hidden"} id="link3">
								<RouteDescriptionView data={data} />
							</div>
						</div>
					</div>
				</div>
			</div>
		</>
	);
};

export default InfoDetailTabView;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

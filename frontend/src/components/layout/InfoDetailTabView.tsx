import { GetServerSideProps, NextPage } from "next";
import { useState } from "react";
import RouteStopList from "./RouteStopList";

const InfoDetailTabView: NextPage<any> = (props) => {
	const [openTab, setOpenTab] = useState<number>(2);
	const routeStopList = props.routeStopList;

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
							href="#link1"
							role="tablist"
						>
							Biểu đồ giờ
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
							Trạm dừng
						</a>
					</li>
					<li className="-mb-px last:mr-0 flex-auto text-center">
						<a
							className={
								"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
								(openTab === 3 ? "text-gray bg-gray-600" : "text-white-600 bg-gray")
							}
							onClick={(e) => {
								e.preventDefault();
								setOpenTab(3);
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
							<div className={openTab === 1 ? "block " : "hidden"} id="link1">
								{/* <InfoDetailTabView /> */}
								tab1
							</div>
							<div className={openTab === 2 ? "block" : "hidden"} id="link2">
								<RouteStopList routeStopList={routeStopList} />
							</div>
							<div className={openTab === 3 ? "block" : "hidden"} id="link3">
								<p>tab3</p>
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

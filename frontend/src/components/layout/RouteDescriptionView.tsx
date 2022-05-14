import { debounce, cloneDeep } from "lodash";
import { GetServerSideProps, NextPage } from "next";
import { useCallback, useEffect, useState } from "react";
import { ApiUtil, BASE_API_PATH, ConvertStringUnsign } from "~/src/utils/ApiUtil";
import RouteLookupListView from "./RouteLookupListView";
import useMergeState from "~/src/hooks/useMergeState";
import { PathIconDown } from "../pages/svg/Path";
import _ from "lodash";

interface IState {}
const RouteDescriptionView: NextPage<any> = (props) => {
	const [openTab, setOpenTab] = useState<number>(1);
	const backwardRouteStops = props?.data?.backwardRouteStops;
	const forwardRouteStops = props?.data?.forwardRouteStops;
	const routeInfo = props?.data?.routeInfo;
	const fieldName = {
		routeCode: "Tuyến số : ",
		name: "Tên Tuyến : ",
		busType: "Độ dài tuyến : ",
		timeRange: "Thời gian chạy : ",
		routeRange: "Số ghế : ",
		unit: "Đơn vị : ",
	};
	const keyFieldName = Object.keys(fieldName);

	const renderBackAndForwardInfo = () => {
		let nameBack = "";
		let nameForward = "";

		backwardRouteStops?.map((item: any, index: number) => {
			nameBack += index === _.get(backwardRouteStops, "length") - 1 ? _.get(item, "name") : _.get(item, "name") + " -> ";
		});
		forwardRouteStops?.map((item: any, index: number) => {
			nameForward += index === _.get(forwardRouteStops, "length") - 1 ? _.get(item, "name") : _.get(item, "name") + " -> ";
		});

		return (
			<>
				<div className="flex font-medium">
					<div className=" flex-1 flex flex-col">
						<div className="flex">
							<div className="pl-2">
								<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24.394 24.394" className="sm:w-2 sm:h-6 mr-2">
									<circle cx="12.197" cy="12.197" r="12.197" />
								</svg>
							</div>
							<div className="pl-4">
								<p className="text-lg">Luợt đi : {nameBack}</p>
							</div>
						</div>
						<div className="flex">
							<div className="pl-2">
								<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24.394 24.394" className="sm:w-2 sm:h-6 mr-2">
									<circle cx="12.197" cy="12.197" r="12.197" />
								</svg>
							</div>
							<div className="pl-4">
								<p className="text-lg">Luợt về : {nameForward}</p>
							</div>
						</div>
					</div>
				</div>
			</>
		);
	};
	// Object.keys()
	const renderRouteInfo = () => {
		return (
			<>
				<>
					{keyFieldName.map((item) => {
						return (
							<>
								<div className="flex font-medium">
									<div className=" flex-1 flex flex-col">
										<div className="flex">
											<div className="pl-2">
												<svg
													xmlns="http://www.w3.org/2000/svg"
													viewBox="0 0 24.394 24.394"
													className="sm:w-2 sm:h-6 mr-2"
												>
													<circle cx="12.197" cy="12.197" r="12.197" />
												</svg>
											</div>
											<div className="pl-4">
												<p className="text-lg">
													{_.get(fieldName, item)} {_.get(routeInfo, item)}
												</p>
											</div>
										</div>
									</div>
								</div>
							</>
						);
					})}
				</>
			</>
		);
	};

	return (
		<>
			<div style={{ height: "calc(100vh - 230px)" }} className="overflow-y-scroll container mb-2  w-full items-center justify-center">
				{renderRouteInfo()}
				{renderBackAndForwardInfo()}
			</div>
		</>
	);
};

export default RouteDescriptionView;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

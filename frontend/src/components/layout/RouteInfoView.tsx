import { debounce, cloneDeep } from "lodash";
import { GetServerSideProps, NextPage } from "next";
import { useCallback, useEffect, useState } from "react";
import { ApiUtil, BASE_API_PATH, ConvertStringUnsign } from "~/src/utils/ApiUtil";
import RouteInfoDetailView from "./RouteInfoDetailView";
import RouteLookupListView from "./RouteLookupListView";
import useMergeState from "~/src/hooks/useMergeState";

interface IState {
	isAllList: boolean;
	infoRouteDetail: any;
}
const RouteInfoView: NextPage<any> = (props) => {
	const [openTab, setOpenTab] = useState<number>(1);
	// const [isAllList, setIsAllList] = useState<boolean>(true);
	const [state, setState] = useMergeState<IState>({
		isAllList: true,
		infoRouteDetail: null,
	});

	useEffect(() => {}, []);

	const handleOnChange = (event: any) => {};

	const handleOnChangeDiv = (RouteId: number) => {
		console.log("RouteId", RouteId);
		// const params = ApiUtil.serialize({ routeId: RouteId });
		ApiUtil.Axios.get(BASE_API_PATH + `/route/get-route-info/` + RouteId).then((res) => {
			console.log("res-routeId", res);
			if (res.data?.success) {
				const data = res.data?.result;
				setState({
					isAllList: false,
					infoRouteDetail: data,
				});
			}
		});
	};

	const handleOnChangeBack = () => {
		setState({
			isAllList: true,
			infoRouteDetail: null,
		});
	};

	return (
		<>
			<div className="flex flex-wrap">
				<div style={{ display: state.isAllList ? "unset" : "none" }} className="h-screen w-full">
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
								Tra cứu
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
								đường
							</a>
						</li>
					</ul>
					<div className=" relative flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
						<div className=" px-4 py-5 flex-auto">
							<div className=" tab-content tab-space">
								<div className={openTab === 1 ? "block " : "hidden"} id="link1">
									<RouteLookupListView handleOnChangeDiv={handleOnChangeDiv} />
								</div>
								<div className={openTab === 2 ? "block" : "hidden"} id="link2">
									<p>a</p>
								</div>
							</div>
						</div>
					</div>
				</div>

				<div style={{ display: state.isAllList ? "none" : "unset" }} className="h-screen w-full">
					<RouteInfoDetailView data={state.infoRouteDetail} handleOnChangeBack={handleOnChangeBack} />
				</div>
			</div>
		</>
	);
};

export default RouteInfoView;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

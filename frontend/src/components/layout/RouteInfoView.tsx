import { debounce, cloneDeep } from "lodash";
import { GetServerSideProps, NextPage } from "next";
import { useCallback, useEffect, useState } from "react";
import { ApiUtil, BASE_API_PATH, ConvertStringUnsign } from "~/src/utils/ApiUtil";
import { pathIconBus_1, pathIconBus_2, pathIconTime_1, pathIconTime_2, PathIconMoney } from "../pages/svg/Path";
import RouteLookupListView from "./RouteLookupListView";

const RouteInfoView: NextPage<any> = ({ props }) => {
	const [openTab, setOpenTab] = useState<number>(1);
	const [isAllList, setIsAllList] = useState<boolean>(true);

	useEffect(() => {}, []);

	const handleOnChange = (event: any) => {};

	const handleOnChangeDiv = (RouteId: number) => {
		console.log("RouteId", RouteId);
		// const params = ApiUtil.serialize({ routeId: RouteId });
		ApiUtil.Axios.get(BASE_API_PATH + `/route/get-route-info/` + RouteId).then((res) => {
			console.log("res-routeId", res);
			setIsAllList(false);
		});
	};

	return (
		<>
			<div className="flex flex-wrap">
				<div style={{ display: isAllList ? "unset" : "none" }} className="h-screen w-full">
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
									<RouteLookupListView check={"as"} handleOnChangeDiv={handleOnChangeDiv} />
								</div>
								<div className={openTab === 2 ? "block" : "hidden"} id="link2">
									<p>a</p>
								</div>
							</div>
						</div>
					</div>
				</div>

				<div style={{ display: isAllList ? "none" : "unset" }} className="h-screen w-full">
					View Chi tiết
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

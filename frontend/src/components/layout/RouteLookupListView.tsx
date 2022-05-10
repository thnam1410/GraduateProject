import { debounce, cloneDeep } from "lodash";
import { GetServerSideProps, NextPage } from "next";
import { useCallback, useEffect, useState } from "react";
import { ApiUtil, BASE_API_PATH, ConvertStringUnsign } from "~/src/utils/ApiUtil";
import { pathIconBus_1, pathIconBus_2, pathIconTime_1, pathIconTime_2, PathIconMoney } from "../pages/svg/Path";

interface IGetMainRoute {
	id: number;
	name: string;
	type: string;
	busType: string;
	routeRange: string;
	timeRange: string;
	unit: string;
	routeCode: string;
	routeDetail: string;
}
interface Props {
	check?: string;
}
let infoRouteArray: IGetMainRoute[] = [];

const RouteLookupListView: NextPage<any> = (props) => {
	const [infoRoutes, setInfoRoutes] = useState<IGetMainRoute[]>([]);
	const [isAllList, setIsAllList] = useState<boolean>(true);
	// const [infoRoutes, setInfoRoutes] = useState<IGetMainRoute[]>([]);

	useEffect(() => {
		ApiUtil.Axios.get(BASE_API_PATH + "/route/get-main-routes").then((res) => {
			const result = res?.data?.result as Array<IGetMainRoute>;
			infoRouteArray = cloneDeep(result);
			setInfoRoutes(result);
		});
	}, []);

	const handleOnChange = (event: any) => {
		const { value } = event.target;
		let valueSearch: IGetMainRoute[] = [];
		let valueConvert = ConvertStringUnsign(value);
		infoRouteArray.map((item) => {
			const nameInfoConvert = ConvertStringUnsign(item.name);
			const routeCodeConvert = ConvertStringUnsign("Tuyến số " + item.routeCode);
			if (nameInfoConvert.includes(valueConvert) || routeCodeConvert.includes(valueConvert)) {
				valueSearch.push(item);
			}
		});
		setInfoRoutes(valueSearch);
	};

	const renderList = () => {
		return (
			<>
				{infoRoutes.map((infoRoute, idx) => {
					return (
						<>
							<li
								onClick={() => {
									props.handleOnChangeDiv(infoRoute.id);
								}}
								className="cursor-pointer mb-2 border-gray-400 flex flex-row"
							>
								<div className="select-nonetems-center w-full duration-500  hover:-translate-y-2 rounded-2xl  border-2 p-3 mt-3 border-black-1000 hover:shadow-2xl">
									<div className="flex font-medium">
										<div className="flex w-1/5">
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
										<div className="flex-1 flex flex-col">
											<p className="text-md leading-3 text-green-600 font-bold">Tuyến số {infoRoute?.routeCode}</p>
											<p className="text-sm font-bold leading-4 text-black/80">{infoRoute?.name}</p>
											<div className="flex flex-1 flex-col">
												<div className="flex flex-1">
													<svg
														xmlns="http://www.w3.org/2000/svg"
														viewBox="0 0 304.547 304.547"
														className="sm:w-6 sm:h-4 mr-2"
													>
														<path d={pathIconTime_1} />
														<path d={pathIconTime_2} />
													</svg>
													<p className="text-sm leading-4">{infoRoute?.timeRange}</p>
												</div>
												<div className="flex flex-1">
													<svg
														xmlns="http://www.w3.org/2000/svg"
														viewBox="0 0 489.038 489.038"
														className="sm:w-6 sm:h-4 mr-2"
													>
														<path d={PathIconMoney} />
													</svg>
													<p className="text-sm leading-4">7,000 VNĐ</p>
												</div>
											</div>
										</div>
									</div>
								</div>
							</li>
						</>
					);
				})}
			</>
		);
	};

	return (
		<>
			<div>
				<input
					type="search"
					id="default-search"
					className="block p-4 pl-8 w-full text-sm text-gray-900 bg-white-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-200 dark:border-gray-600 dark:placeholder-gray-400 dark:text-black dark:focus:ring-blue-500 dark:focus:border-blue-500"
					placeholder="Tìm tuyến xe"
					onChange={debounce(handleOnChange, 1000)}
					required
				/>

				<div className="container mb-2  w-full items-center justify-center">
					<ul style={{ height: "calc(100vh - 180px)" }} className="overflow-y-scroll flex flex-col p-3">
						{renderList()}
					</ul>
				</div>
			</div>
		</>
	);
};

export default RouteLookupListView;
// export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
// 	return {
// 		props: {},
// 	};
// };

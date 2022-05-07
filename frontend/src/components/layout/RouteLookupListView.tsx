import { GetServerSideProps, NextPage } from "next";
import { useEffect, useState } from "react";
import { ApiUtil, BASE_API_PATH } from "~/src/utils/ApiUtil";

interface IGetMainRoute {
	name: string;
	type: string;
	busType: string;
	routeRange: string;
	timeRange: string;
	unit: string;
	routeCode: string;
	routeDetail: string;
}

const RouteLookupListView: NextPage<any> = ({ props }) => {
	const [infoRoutes, setInfoRoutes] = useState<any[]>([]);

	useEffect(() => {
		ApiUtil.Axios.get(BASE_API_PATH + "/route/get-main-routes").then((res) => {
			const result = res?.data?.result as Array<IGetMainRoute>;
			console.log("result", result);
			setInfoRoutes(result);
		});
	}, []);
	const renderList = () => {
		return (
			<>
				{infoRoutes.map((infoRoute, idx) => {
					return (
						<>
							<li className=" mb-2 border-gray-400 flex flex-row">
								<div
									style={{ width: "420px" }}
									className="select-nonetems-center  duration-500  hover:-translate-y-2 rounded-2xl  border-2 p-3 mt-3 border-black-1000 hover:shadow-2xl"
								>
									<div style={{ width: "350px" }} className="flex font-medium">
										<div className="flex-none w-14">
											<div className="bg-blue-light shadow-border p-3">
												<div className="w-4 h-4">
													<svg
														xmlns="http://www.w3.org/2000/svg"
														className="w-4 h-4 mx-1 sm:w-6 sm:h-6"
														viewBox="0 0 472.666 472.666"
													>
														<path d="M385.996 48.833a7.5 7.5 0 0 0-7.5-7.5H94.17c-4.143 0-7.5 3.358-7.5 7.5s3.357 7.5 7.5 7.5h284.326a7.5 7.5 0 0 0 7.5-7.5zM284.964 312.708h-97.262c-4.143 0-7.5 3.358-7.5 7.5s3.357 7.5 7.5 7.5h97.262a7.5 7.5 0 0 0 0-15z" />
														<path d="M452.166 81.333h-26.17v-27.5c0-28.949-23.552-52.5-52.5-52.5H99.17c-28.948 0-52.5 23.551-52.5 52.5v27.5H20.5c-11.304 0-20.5 9.196-20.5 20.5v60.881c0 11.304 9.196 20.5 20.5 20.5h3.67c4.143 0 7.5-3.358 7.5-7.5s-3.357-7.5-7.5-7.5H20.5a5.506 5.506 0 0 1-5.5-5.5v-60.881c0-3.033 2.468-5.5 5.5-5.5h26.17v305.25c0 6.963 4.098 12.972 10 15.787v46.463a7.5 7.5 0 0 0 7.5 7.5h64.145a7.5 7.5 0 0 0 7.5-7.5v-22.25c0-4.142-3.357-7.5-7.5-7.5s-7.5 3.358-7.5 7.5v14.75H71.67v-37.25h329.326v37.25h-49.144v-14.75c0-4.142-3.357-7.5-7.5-7.5s-7.5 3.358-7.5 7.5v22.25a7.5 7.5 0 0 0 7.5 7.5h64.144a7.5 7.5 0 0 0 7.5-7.5V417.37c5.902-2.816 10-8.824 10-15.787V96.333h26.17c3.032 0 5.5 2.467 5.5 5.5v60.881c0 3.033-2.468 5.5-5.5 5.5h-3.67c-4.143 0-7.5 3.358-7.5 7.5s3.357 7.5 7.5 7.5h3.67c11.304 0 20.5-9.196 20.5-20.5v-60.881c0-11.304-9.196-20.5-20.5-20.5zM61.67 53.833c0-20.678 16.822-37.5 37.5-37.5h274.326c20.678 0 37.5 16.822 37.5 37.5v27.5H61.67v-27.5zm349.326 308.875h-71.033c-9.649 0-17.5-7.851-17.5-17.5v-17.5h66.033c4.143 0 7.5-3.358 7.5-7.5s-3.357-7.5-7.5-7.5h-73.533a7.5 7.5 0 0 0-7.5 7.5v25c0 17.92 14.579 32.5 32.5 32.5h71.033v23.875c0 1.355-1.145 2.5-2.5 2.5H64.17c-1.355 0-2.5-1.145-2.5-2.5v-23.875h71.033c17.921 0 32.5-14.58 32.5-32.5v-25a7.5 7.5 0 0 0-7.5-7.5H84.17c-4.143 0-7.5 3.358-7.5 7.5s3.357 7.5 7.5 7.5h66.033v17.5c0 9.649-7.851 17.5-17.5 17.5H61.67V96.333h167.163v185H84.17c-4.143 0-7.5 3.358-7.5 7.5s3.357 7.5 7.5 7.5h304.326c4.143 0 7.5-3.358 7.5-7.5s-3.357-7.5-7.5-7.5H243.833v-185h167.163v266.375z" />
													</svg>
												</div>
											</div>
										</div>
										<div className="flex-initial w-64 ...">02</div>
										<div className="flex-initial w-32 ...">03</div>
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
		<div>
			<input
				type="search"
				id="default-search"
				className="block p-4 pl-8 w-full text-sm text-gray-900 bg-white-50 rounded-lg border border-gray-300 focus:ring-blue-500 focus:border-blue-500 dark:bg-gray-200 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
				placeholder="Tìm tuyến xe"
				onChange={() => {}}
				required
			/>

			<div className="overflow-auto container mb-2 flex mx-auto w-full items-center justify-center">
				<ul className="overflow-y-scroll flex flex-col p-3" style={{ height: 610 }}>
					{renderList()}
				</ul>
			</div>
		</div>
	);
};

export default RouteLookupListView;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

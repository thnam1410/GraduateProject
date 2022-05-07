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
	});
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
									<div className="font-medium">Tuyến xe số aaaaaaaaaaaaaaaaâaaaaaaaa</div>
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

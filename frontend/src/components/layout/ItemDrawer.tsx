import { NextPage } from "next";
import { pathIconBus_1, pathIconBus_2, PathMagnifyingGlass } from "../pages/svg/Path";

const ItemDrawer: NextPage<any> = (props) => {
	const { data } = props;

	const renderBody = () => {
		return (
			<>
				<div className=" flex space-x-4">{data.isSearch ? renderIsSearch() : renderRouteInfo()}</div>
			</>
		);
	};

	const renderRouteInfo = () => {
		console.log("data", data);
		return (
			<>
				<div className="animate-pulse rounded-full bg-gray-400 h-12 w-12">
					<div className={"icon"} style={{ width: 65 }}>
						<div className="bg-blue-light shadow-border p-3 w-4 h-4">
							<svg xmlns="http://www.w3.org/2000/svg" className="w-4 h-4 sm:w-6 sm:h-6 mr-2" viewBox="0 0 472.666 472.666">
								<path d={pathIconBus_1} />
								<path d={pathIconBus_2} />
							</svg>
						</div>
					</div>
				</div>
				<div className="flex-1 space-y-4 py-1">
					<p className="text-sm font-bold leading-4 text-black/80">Tuyến số : {data?.route?.routeCode}</p>
					<p className="text-sm leading-4 text-black/80">{data?.route?.name}</p>
					<p className="text-sm leading-4 text-black/80">Thời gian chạy : {data?.route?.timeRange}</p>
					<p className="text-sm leading-4 text-black/80">Thời gian tìm kiếm : {data?.timeSearchDto}</p>
				</div>
			</>
		);
	};

	const renderIsSearch = () => {
		return (
			<>
				<div className="animate-pulse rounded-full bg-gray-400 h-12 w-12">
					<div className={"icon"} style={{ height: "100%", display: "flex", justifyContent: "center" }}>
						<svg
							xmlns="http://www.w3.org/2000/svg"
							viewBox="0 0 472.666 472.666"
							style={{ marginTop: "10px" }}
							className="w-4 h-4 sm:w-6 sm:h-6 mr-2"
						>
							<path d={PathMagnifyingGlass} />
						</svg>
					</div>
				</div>
				<div className="flex-1 space-y-4 py-1">
					<p className="text-sm leading-4 text-black/80">Điểm đón : {data?.departPoint}</p>
					<p className="text-sm leading-4 text-black/80">Điểm đến : {data?.destination}</p>
					<p className="text-sm leading-4 text-black/80">Thời gian tìm kiếm : {data?.timeSearchDto}</p>
				</div>
			</>
		);
	};

	return (
		<div className="mb-4" style={{ display: "flex", justifyContent: "center" }}>
			<div className="border border-gray-300 shadow rounded-md p-4 max-w-sm w-full mx-auto">{renderBody()}</div>
		</div>
	);
};
export default ItemDrawer;

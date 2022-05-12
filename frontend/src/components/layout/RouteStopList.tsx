import { GetServerSideProps, NextPage } from "next";
import { useState } from "react";

const RouteStopList: NextPage<any> = (props) => {
	const routeStopList: [] = props.routeStopList || [];
	console.log("routeStopList", routeStopList);
	return (
		<>
			<ul style={{ height: "calc(100vh - 230px)" }} className="overflow-y-scroll flex flex-col p-3">
				{routeStopList.map((item: any) => {
					return (
						<>
							<li
								onClick={() => {
									// props.handleOnChangeDiv(infoRoute.id);
								}}
								className="cursor-pointer border-gray-400 flex flex-row"
							>
								<div className="flex">
									<div className="pl-2 l-2">
										<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24.394 24.394" className="sm:w-2 sm:h-2 mt-2">
											<circle cx="12.197" cy="12.197" r="12.197" />
										</svg>

										<svg width="16" height="16" xmlns="http://www.w3.org/2000/svg">
											<path fill="#444" d="M8 0h1v16H8V0z" />
										</svg>
									</div>
									<div className="pl-4">
										<p className="text-lg">{item.name}</p>
									</div>
								</div>
							</li>
						</>
					);
				})}
			</ul>
		</>
	);
};

export default RouteStopList;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

import { GetServerSideProps, NextPage } from "next";
import { useState } from "react";
import { PathIconDown } from "../pages/svg/Path";
const RouteStopList: NextPage<any> = (props) => {
	const routeStopList: [] = props.routeStopList || [];
	return (
		<>
			<ul style={{ height: "calc(100vh - 230px)" }} className="overflow-y-scroll flex flex-col p-3">
				{routeStopList.map((item: any, index: number) => {
					return (
						<>
							<li
								onClick={() => {
									// props.handleOnChangeDiv(infoRoute.id);
								}}
								className="cursor-pointer border-gray-400 flex flex-row"
							>
								<div className="flex">
									<div className="pl-2">
										{index === 0 || index === routeStopList.length - 1 ? (
											<svg
												xmlns="http://www.w3.org/2000/svg"
												viewBox="0 0 24.394 24.394"
												className="sm:w-6 sm:h-3 mr-2"
											>
												<circle cx="12.197" cy="12.197" r="12.197" />
											</svg>
										) : (
											<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 490 490" className="sm:w-6 sm:h-4 mr-2">
												<path d={PathIconDown} />
											</svg>
										)}
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

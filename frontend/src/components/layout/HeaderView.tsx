import { GetServerSideProps, NextPage } from "next";
import Link from "next/link";
import { signOut } from "next-auth/react";

import { urlUser } from "../pages/svg/UrlImage";
import {
	PathIconBusHeader1,
	PathIconBusHeader2,
	PathIconBusHeader3,
	PathIconBusHeader4,
	PathIconBusHeader5,
	PathIconBusHeader6,
} from "../pages/svg/Path";
import { useEffect, useState } from "react";
import { useStore } from "~/src/zustand/store";
import { useRouter } from "next/router";
const HeaderView: NextPage<any> = ({ children }) => {
	const userSession = useStore((state) => state.userSession);
	const [showOptions, setShowOptions] = useState<boolean>(false);
	useEffect(() => {}, [userSession]);
	const handleClick = () => {
		setShowOptions(!showOptions);
	};
	const router = useRouter();

	const onLogout = async () => {
		const data = await signOut({ redirect: false, callbackUrl: "/auth/login" });
		router.push(data.url);
	};

	const renderRightLogin = () => {
		return (
			<>
				{userSession !== null ? (
					<>
						<div className="mt-3 mr-4 relative inline-block text-left">
							{/* <div>
								<button
									onClick={handleClick}
									type="button"
									className=" inline-flex justify-center w-full rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-sm font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-offset-gray-100 focus:ring-indigo-500"
									id="menu-button"
									aria-expanded="true"
									aria-haspopup="true"
								>
									{userSession?.user?.fullName}
									<svg
										className="-mr-1 ml-2 h-5 w-5"
										xmlns="http://www.w3.org/2000/svg"
										viewBox="0 0 20 20"
										fill="currentColor"
										aria-hidden="true"
									></svg>
								</button>

							</div> */}
							<div onClick={handleClick} className="flex justify-center items-center space-x-3 cursor-pointer">
								<div className="w-8 h-8 rounded-full overflow-hidden border-2 dark:border-white border-gray-900">
									<img src={urlUser} alt="" className="w-full h-full object-cover" />
								</div>
								<div className="font-semibold dark:text-white text-gray-900 text-lg">
									<div className="cursor-pointer">{userSession?.user?.fullName}</div>
								</div>
							</div>
							{showOptions && (
								<div
									className="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none"
									style={{ zIndex: 3, backgroundColor: "aqua" }}
									role="menu"
									aria-orientation="vertical"
									aria-labelledby="menu-button"
									tabIndex={-1}
								>
									<ul className="space-y-3 dark:text-white">
										<hr className="dark:border-gray-700" />
										<li className="font-medium">
											<div
												onClick={onLogout}
												className="cursor-pointer justify-center flex items-center transform transition-colors duration-200 border-r-4 border-transparent hover:border-red-600"
											>
												<div className="mr-3 text-red-600">
													<svg
														className="w-6 h-6"
														fill="none"
														stroke="currentColor"
														viewBox="0 0 24 24"
														xmlns="http://www.w3.org/2000/svg"
													>
														<path
															strokeLinecap="round"
															strokeLinejoin="round"
															strokeWidth="2"
															d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1"
														></path>
													</svg>
												</div>
												Logout
											</div>
										</li>
									</ul>
								</div>
							)}
						</div>
					</>
				) : (
					<>
						<Link href={"/auth/login"}>
							<button
								type="button"
								className="mt-3 mr-4 text-white bg-gradient-to-r from-cyan-400 via-cyan-500 to-cyan-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-cyan-300 dark:focus:ring-cyan-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center"
							>
								Đăng nhập
							</button>
						</Link>
						<Link href={"/auth/register"}>
							<button
								type="button"
								className="mt-3 mr-8 text-white bg-gradient-to-r from-teal-400 via-teal-500 to-teal-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-teal-300 dark:focus:ring-teal-800 font-medium rounded-lg text-sm px-5 py-2.5 text-center"
							>
								Đăng ký
							</button>
						</Link>
					</>
				)}
			</>
		);
	};

	return (
		<>
			<div className="flex w-1/5 pb-2">
				<div className="bg-blue-light shadow-border p-3 w-4 h-4">
					<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512.002 512.002" className=" sm:w-12 sm:h-12 mr-3">
						<path style={{ fill: "#ffad61" }} d={PathIconBusHeader1} />
						<circle style={{ fill: "#666" }} cx="128.5" cy="338.002" r="46.294" />
						<circle style={{ fill: "#666" }} cx="377.299" cy="338.002" r="46.294" />
						<circle style={{ fill: "#f2f2f2" }} cx="128.5" cy="338.002" r="14.814" />
						<circle style={{ fill: "#f2f2f2" }} cx="377.299" cy="338.002" r="14.814" />
						<path style={{ fill: "#f2f2f2" }} d={PathIconBusHeader2} />
						<path style={{ fill: "#73c1dd" }} d={PathIconBusHeader3} />
						<path style={{ fill: "#f2f2f2" }} d="M221.792 203.209h77.698V339.43h-77.698z" />
						<path style={{ fill: "#4d3d36" }} d={PathIconBusHeader4} />
						<path style={{ fill: "#4d3d36" }} d={PathIconBusHeader5} />
						<path style={{ fill: "#4d3d36" }} d={PathIconBusHeader6} />
					</svg>
				</div>
				<div className="bg-blue-light shadow-border ml-12 mt-4">
					<p className="text-3xl text-white m-0">BusMap</p>
				</div>
				<div className="absolute top-0 right-0">{renderRightLogin()}</div>
			</div>
		</>
	);
};

export default HeaderView;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

import { GetServerSideProps, NextPage } from "next";
import Link from "next/link";
import { useSession } from "next-auth/react";
import { UserSession } from "../../types/UserInfo";
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
const HeaderView: NextPage<any> = ({ children }) => {
	const userSession = useStore((state) => state.userSession);
	const [showOptions, setShowOptions] = useState<boolean>(false);
	useEffect(() => {}, [userSession]);
	const handleClick = () => {
		setShowOptions(!showOptions);
	};

	const renderRightLogin = () => {
		return (
			<>
				{userSession !== null ? (
					<>
						<div className="mt-3 mr-4 relative inline-block text-left">
							<div>
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
							</div>

							{showOptions && (
								<div
									className="origin-top-right absolute right-0 mt-2 w-56 rounded-md shadow-lg bg-white ring-1 ring-black ring-opacity-5 focus:outline-none"
									style={{ zIndex: 3 }}
									role="menu"
									aria-orientation="vertical"
									aria-labelledby="menu-button"
									tabIndex={-1}
								>
									<div className="py-1" role="none">
										<form method="POST" action="#" role="none">
											<button
												type="submit"
												className="text-gray-700 block w-full text-left px-4 py-2 text-sm"
												role="menuitem"
												tabIndex={-1}
												id="menu-item-3"
											>
												Đăng xuất
											</button>
										</form>
									</div>
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

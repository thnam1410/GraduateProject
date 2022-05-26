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
import { useEffect } from "react";
import { useStore } from "~/src/zustand/store";
const HeaderView: NextPage<any> = ({ children }) => {
	const userSession = useStore((state) => state.userSession);

	useEffect(() => {}, [userSession]);

	const renderRightLogin = () => {
		console.log("userSession", userSession);
		return (
			<>
				{userSession !== null ? (
					"Đã login"
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
			<div className="flex w-1/5">
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
					<p className="text-3xl text-white">BusMap</p>
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

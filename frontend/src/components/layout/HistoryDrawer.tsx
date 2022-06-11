import { NextPage } from "next";
import { useEffect } from "react";
import { RouteInfoSearchView } from "~/src/types/InfoRouteSearch";
import { ApiUtil, BASE_API_PATH } from "~/src/utils/ApiUtil";
import { useStore } from "~/src/zustand/store";
import ItemDrawer from "./ItemDrawer";
import _ from "lodash";
import { useSession } from "next-auth/react";
import { UserSession } from "~/src/types/UserInfo";

const HisotryDrawer: NextPage<any> = (props) => {
	const { children } = props;
	const isOpen = useStore((state) => state.isOpen);
	const setIsOpen = useStore((state) => state.setIsOpen);
	const infoRouteSearch: any = useStore((state) => state.infoRouteSearch);
	const setInfoRouteSearch = useStore((state) => state.setInfoRouteSearch);
	const session = useSession();
	const user = session?.data?.user as UserSession;
	console.log("🚀 ~ file: HistoryDrawer.tsx ~ line 19 ~ user", user);
	useEffect(() => {
		if (infoRouteSearch != null || !user) return;
		handleOnChangeRoute(user?.user?.id);
	}, [infoRouteSearch, user]);

	const handleOnChangeRoute = (userId: string) => {
		ApiUtil.Axios.get(BASE_API_PATH + `/route/get-route-info-search/` + userId)
			.then((res) => {
				if (res.data?.success) {
					const data = res.data?.result as RouteInfoSearchView[];
					console.log("🚀 ~ file: HistoryDrawer.tsx ~ line 24 ~ .then ~ data", data);
					setInfoRouteSearch(data);
				}
			})
			.catch((err) => {
				console.log("err", err);
			});
	};

	const renderItemDrawer = () => {
		console.log("infoRouteSearch", infoRouteSearch);
		return (
			<>
				{_.map(infoRouteSearch, (itemParent) => {
					return (
						<>
							{_.map(itemParent.infoRouteSearchList, (item) => {
								return (
									<>
										<ItemDrawer data={item} />
									</>
								);
							})}
						</>
					);
				})}
			</>
		);
	};

	return (
		<>
			<main
				className={
					" absolute overflow-hidden z-10 bg-white inset-0 transform ease-in-out  transition-opacity duration-500 translate-x-0"
				}
			>
				<article className="relative max-w-lg flex flex-col space-y-6 overflow-y-scroll h-full">
					<p className="p-4 font-bold text-lg">Lịch sử tìm kiếm</p>
					{renderItemDrawer()}
				</article>
			</main>
		</>
	);
};
export default HisotryDrawer;

import { debounce, cloneDeep } from "lodash";
import { GetServerSideProps, NextPage } from "next";
import {useCallback, useEffect, useRef, useState} from "react";
import { ApiUtil, BASE_API_PATH, ConvertStringUnsign } from "~/src/utils/ApiUtil";
import RouteInfoDetailView from "./RouteInfoDetailView";
import RouteLookupListView from "./RouteLookupListView";
import useMergeState from "~/src/hooks/useMergeState";
import { useStore } from "~/src/zustand/store";
import SearchView from "~/src/components/layout/SearchView";
import { useMapControlStore } from "~/src/zustand/MapControlStore";
import {useMapControlStoreV2} from '~/src/zustand/MapControlStoreV2';

interface IState {
	isAllList: boolean;
	infoRouteDetail: any;
}

const RouteInfoView: NextPage<any> = (props) => {
	const [openTab, setOpenTab] = useState<number>(1);
	const divRef = useRef<HTMLDivElement>(null);
	const isAllList = useStore((state) => state.isAllList);
	const switchMap = useMapControlStoreV2((state) => state.switchMap);

	return (
		<>
			<div className="flex flex-wrap h-full w-full overflow-hidden" style={{ minWidth: 350 }}>
				<div style={{ display: isAllList ? "unset" : "none" }} className="h-full w-full">
					<ul className="flex mb-0 list-none flex-wrap pt-3 pb-4 flex-row" role="tablist">
						<li className="-mb-px last:mr-0 flex-auto text-center">
							<a
								className={
									"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
									(openTab === 1 ? "text-white bg-blue-600" : "text-blue-600 bg-white")
								}
								onClick={(e) => {
									e.preventDefault();
									if (openTab === 1) return;
									setOpenTab(1);
									switchMap();
								}}
								data-toggle="tab"
								href="#link1"
								role="tablist"
							>
								Tra cứu
							</a>
						</li>
						<li className="-mb-px last:mr-0 flex-auto text-center">
							<a
								className={
									"text-xs font-bold uppercase px-5 py-3 shadow-lg rounded block leading-normal " +
									(openTab === 2 ? "text-white bg-blue-600" : "text-blue-600 bg-white")
								}
								onClick={(e) => {
									e.preventDefault();
									if (openTab === 2) return;
									setOpenTab(2);
									switchMap();
								}}
								data-toggle="tab"
								href="#link2"
								role="tablist"
							>
								Đường
							</a>
						</li>
					</ul>
					<div ref={divRef} className="parent-div relative h-full flex flex-col min-w-0 break-words bg-white w-full mb-6 shadow-lg rounded">
						<div className="px-4 py-5 flex-auto">
							<div className="tab-content tab-space h-full w-full">
								<div className={openTab === 1 ? "block w-full h-full" : "hidden"} id="link1">
									<RouteLookupListView parentDivRef={divRef} />
								</div>
								<div className={openTab === 2 ? "block w-full h-full" : "hidden"} id="link2">
									<SearchView />
								</div>
							</div>
						</div>
					</div>
				</div>

				{!isAllList && (
					<div className="h-full w-full">
						<RouteInfoDetailView />
					</div>
				)}
			</div>
		</>
	);
};

export default RouteInfoView;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

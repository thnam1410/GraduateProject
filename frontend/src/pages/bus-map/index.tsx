import {NextPage} from "next";
import Map from "~/src/components/layout/Map";
import MarkerMulti from "./RoutingMachine";
import useBrowser from "~/src/hooks/useBrowser";
import React from "react";

const BusMap: NextPage<any> = (props) => {
	const checkBrowser = useBrowser();
	if (!checkBrowser) return null;
	return (
		<>
			<div id="map" style={{ position: "relative" }}>
				<Map/>
			</div>
		</>
	);
};

export default BusMap;

// export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
// 	const checkBrowser = useBrowser();
// 	if (!checkBrowser) return null;
// 	return {
// 		props: {},
// 	};
// };

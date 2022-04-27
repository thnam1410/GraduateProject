import { NextPage } from "next";
import React from "react";
import useBrowser from "~/src/hooks/useBrowser";
import MapContainer from "../../components/layout/Map";

const BusMap: NextPage<any> = (props) => {
	return (
		<></>
		// <div id="map" style={{ position: "relative" }}>
		// 	<Map />
		// </div>
	);
};
export default BusMap;

// @ts-ignore
BusMap.getLayout = function getLayout(page: ReactElement) {
	return (
		<div id="map" style={{ position: "relative" }}>
			<MapContainer>{page}</MapContainer>
		</div>
	);
};

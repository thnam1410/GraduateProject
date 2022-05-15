import {NextPage} from "next";
import BusMap from "~/src/components/layout/BusMap";
import useBrowser from "~/src/hooks/useBrowser";
import React from "react";

const BusMapPage: NextPage<any> = (props) => {
	const checkBrowser = useBrowser();
	if (!checkBrowser) return null;
	return (
		<>
			<div id="map" style={{ position: "relative" }}>
				<BusMap/>
			</div>
		</>
	);
};

export default BusMapPage;

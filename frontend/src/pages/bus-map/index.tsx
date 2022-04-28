import { GetServerSideProps, NextPage } from "next";
import dynamic from "next/dynamic";
import Map from "~/src/components/layout/Map";
import MarkerMulti from "./RoutingMachine";
import useBrowser from "~/src/hooks/useBrowser";
import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import React, { useEffect, useState } from "react";
import DataTest from "../Data/DataTest";

const BusMap: NextPage<any> = (props) => {
	const checkBrowser = useBrowser();
	if (!checkBrowser) return null;
	return (
		<>
			<div id="map" style={{ position: "relative" }}>
				<Map>
					<MarkerMulti></MarkerMulti>
				</Map>
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

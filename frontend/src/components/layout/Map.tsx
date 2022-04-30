import { MapContainer, TileLayer, Marker, Popup, Polygon, Polyline, useMapEvents, MarkerProps } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import React, { useRef, useState } from "react";
import useBrowser from "~/src/hooks/useBrowser";
import { GetServerSideProps, NextPage } from "next";
import { useLeafletContext } from "@react-leaflet/core";
import { LatLngBoundsExpression, Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";

const Map: NextPage<any> = ({ children }) => {
	const polyLineRef = useRef<LeafletPolyline>(null);
	const leafletMap = useRef<LeafletMap>(null);

	const renderMarkers = () => {
		return (
			<>
				<Marker position={[10.75104046, 106.65258026]} draggable={false}>
					<Popup>Start</Popup>
				</Marker>
				<Marker position={[10.75352001, 106.65074158]} draggable={false}>
					<Popup>End</Popup>
				</Marker>
			</>
		);
	};

	const renderRoutes = () => {
		return (
			<Polyline
				ref={polyLineRef}
				pathOptions={{ color: "purple" }}
				positions={[
					[10.75104046, 106.65258026],
					[10.75094986, 106.65097046],
					[10.75100994, 106.65048218],
					[10.7514801, 106.65052795],
					[10.75234032, 106.65061951],
					[10.75352001, 106.65074158],
				]}
			/>
		);
	};
	return (
		<>
			<div style={{ height: 50, width: "100%" }}>
				<button
					onClick={() => {
						console.log("leafletMap.current", leafletMap.current);
						leafletMap.current?.fitBounds(polyLineRef.current?.getBounds() as LatLngBoundsExpression);
					}}
				>
					Click
				</button>
			</div>
			<MapContainer
				ref={leafletMap}
				center={[10.762622, 106.660172]}
				zoom={14}
				scrollWheelZoom={true}
				style={{ height: "100%", width: "100%" }}
			>
				<TileLayer
					url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
					attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
				/>
				{renderMarkers()}
				{renderRoutes()}
			</MapContainer>
		</>
	);
};

export default Map;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

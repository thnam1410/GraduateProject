import { MapContainer, Marker, Polyline, Popup, TileLayer } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import React, { useEffect, useRef, useState } from "react";
import useBrowser from "~/src/hooks/useBrowser";
import { GetServerSideProps, NextPage } from "next";
import { useLeafletContext } from "@react-leaflet/core";
import { LatLngBoundsExpression, Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";
import { ApiUtil, BASE_API_PATH } from "~/src/utils/ApiUtil";
import GooglePlacesAutocomplete, { geocodeByAddress, geocodeByPlaceId, getLatLng } from "react-google-places-autocomplete";
import AdminLayout from "./AdminLayout";
import { Tabs } from "antd";

const Map: NextPage<any> = ({ children }) => {
	const polyLineRef = useRef<LeafletPolyline>(null);
	const leafletMap = useRef<LeafletMap>(null);
	const [positions, setPosition] = useState<any[]>([]);
	const [address, setAddress] = useState<any>(null);
	const { TabPane } = Tabs;

	useEffect(() => {
		// ApiUtil.Axios.get(BASE_API_PATH + "/route/get-path-by-route-detail-id?routeDetailId=1651").then((res) => {
		// 	const result = res?.data?.result as Array<{ lat: number; lng: number }>;
		// 	const positionResult = result.reduce((acc: any[], curr) => {
		// 		acc.push([curr.lat, curr.lng]);
		// 		return acc;
		// 	}, []);
		// 	setPosition(positionResult);
		// 	setTimeout(() => leafletMap.current?.fitBounds(polyLineRef.current?.getBounds() as LatLngBoundsExpression));
		// });
		const getInfoPlaceId = async () => {
			const geocodeObj = address && address.value ? await geocodeByPlaceId(address.value.place_id) : null;
			if (geocodeObj !== null) {
				let acc = [];
				let lng = geocodeObj[0]?.geometry?.location?.lng();
				let lat = geocodeObj[0]?.geometry?.location?.lat();
				acc.push([lat, lng]);
				setPosition(acc);
			}
		};
		getInfoPlaceId();
	}, [address]);

	const renderMarkers = () => {
		return (
			<>
				{positions.map((position, idx) => {
					switch (idx) {
						case 0:
							return (
								<Marker position={position} draggable={false}>
									<Popup>Start</Popup>
								</Marker>
							);
						case positions.length - 1:
							return (
								<Marker position={position} draggable={false}>
									<Popup>End</Popup>
								</Marker>
							);
					}
				})}
			</>
		);
	};

	const renderRoutes = () => {
		return <Polyline ref={polyLineRef} pathOptions={{ color: "purple" }} positions={positions} />;
	};
	return (
		<>
			<div className="border bg-white-800 absolute duration-500 -left-0 " style={{ width: "27%" }}>
				<Tabs className="flex" defaultActiveKey="1" onChange={() => {}}>
					<TabPane className="flex-1" tab="Tab 1" key="1"></TabPane>
					<TabPane className="flex-1" tab="Tab 2" key="2"></TabPane>
				</Tabs>
			</div>

			<div
				className="absolute inset-y-0 right-0"
				style={{
					height: "100%",
					width: "73%",
				}}
			>
				<div
					style={{
						position: "absolute",
						zIndex: 3,
						width: "40%",
						left: "60px",
						top: "10px",
					}}
				>
					{
						<GooglePlacesAutocomplete
							apiKey="AIzaSyDQXEJEdPF6r1WQMhj9rWW03oyV39kh0Dg"
							selectProps={{
								placeholder: "Nhập vị trí cần chọn",
								isClearable: true,
								value: address,
								onChange: (val: any) => {
									setAddress(val);
								},
							}}
						/>
					}
				</div>
				<MapContainer
					ref={leafletMap}
					center={[10.762622, 106.660172]}
					zoom={14}
					scrollWheelZoom={true}
					style={{ height: "100%", width: "100%", zIndex: 1 }}
				>
					<TileLayer
						url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
						attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
					/>
					{renderMarkers()}
					{renderRoutes()}
				</MapContainer>
			</div>
		</>
	);
};

export default Map;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

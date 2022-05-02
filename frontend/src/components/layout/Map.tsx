import { MapContainer, TileLayer, Marker, Popup, Polygon, Polyline, useMapEvents, MarkerProps } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import React, { useEffect, useRef, useState } from "react";
import { GetServerSideProps, NextPage } from "next";
import { Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";
import DataTest from "~/src/pages/Data/DataTest";
import RoutingMachine from "~/src/pages/bus-map/RoutingMachine";
import GooglePlacesAutocomplete, { geocodeByAddress, geocodeByPlaceId, getLatLng } from "react-google-places-autocomplete";
const Map: NextPage<any> = ({ children }) => {
	const polyLineRef = useRef<LeafletPolyline>(null);
	const leafletMap = useRef<LeafletMap>(null);
	const [address, setAddress] = useState<any>(null);

	useEffect(() => {
		const func = async () => {
			console.log("address", address);
			const geocodeObj = address && address.value && (await geocodeByPlaceId(address.value.place_id));
			console.log("geocodeObj", geocodeObj);
		};
		func();
	}, [address]);

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
					// <GooglePlacesAutocomplete
					// 	selectProps={{
					// 		placeholder: "Nhập vị trí cần chọn",
					// 		value: valueSearch,
					// 		// onChange: (data: any, details = null) => {
					// 		// 	// 'details' is provided when fetchDetails = true
					// 		// 	console.log("details", details);
					// 		// 	console.log("data", data);
					// 		onChange: handleChange,
					// 		onSelect: handleSelect,
					// 		// },
					// 		styles: {
					// 			// placeholder: "Placeholder...",
					// 			input: (provided: any) => ({
					// 				...provided,
					// 				// color: "blue",
					// 			}),
					// 			option: (provided: any) => ({
					// 				...provided,
					// 				// color: "blue",
					// 			}),
					// 			singleValue: (provided: any) => ({
					// 				...provided,
					// 				// color: "blue",
					// 			}),
					// 		},
					// 	}}
					// 	apiKey={"AIzaSyDQXEJEdPF6r1WQMhj9rWW03oyV39kh0Dg"}
					// />
				}
			</div>
			<div
				style={{
					position: "relative",
					zIndex: 1,
					height: "100%",
				}}
			>
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

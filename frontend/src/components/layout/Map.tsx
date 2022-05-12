import { MapContainer, Marker, Polyline, Popup, TileLayer, ZoomControl } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import React, { useEffect, useRef, useState } from "react";
import { GetServerSideProps, NextPage } from "next";
import { Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";
import GooglePlacesAutocomplete, { geocodeByPlaceId } from "react-google-places-autocomplete";
import { Tabs } from "antd";
import RouteInfoView from "./RouteInfoView";
import { debounce } from "lodash";

const SIDE_BAR_WIDTH = 23;
const Map: NextPage<any> = ({ children }) => {
	const polyLineRef = useRef<LeafletPolyline>(null);
	const leafletMap = useRef<LeafletMap>(null);
	const [positions, setPosition] = useState<any[]>([]);
	const [isOpenSideBar, setIsOpenSideBar] = useState(true);

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

	const onChangeAddress = debounce(async (address) => {
		try {
			const geocodeObj = await geocodeByPlaceId(address?.value?.place_id);
			if (geocodeObj !== null) {
				let acc = [];
				let lng = geocodeObj[0]?.geometry?.location?.lng();
				let lat = geocodeObj[0]?.geometry?.location?.lat();
				acc.push([lat, lng]);
				console.log(acc);
			}
		} catch (e) {
			console.log("err", e);
		}
	}, 300);

	return (
		<>
			<div
				className="border bg-white-800 flex items-center absolute duration-500 -left-0"
				style={{
					width: isOpenSideBar ? `${SIDE_BAR_WIDTH}%` : 0,
				}}
			>
				<div
					className="flex m-auto justify-center p-3 items-center absolute py-6 justify-center content-center right-[-30px] h-[20px] w-[20px] bg-white text-black z-10 cursor-pointer rounded bg-gray-300 border-solid border-2 border-sky-500 text-xl"
					onClick={() => {
						setIsOpenSideBar(!isOpenSideBar);
						setTimeout(() => leafletMap.current?.invalidateSize(), 500);
					}}
				>
					{isOpenSideBar ? "<" : ">"}{" "}
				</div>
				<RouteInfoView />
			</div>

			<div
				className="h-screen absolute inset-y-0 right-0 duration-500"
				style={{
					width: isOpenSideBar ? `${100 - SIDE_BAR_WIDTH}%` : "100%",
				}}
			>
				<div
					style={{
						position: "absolute",
						zIndex: 3,
						width: "40%",
						left: "30px",
						top: "10px",
					}}
				>
					{
						<GooglePlacesAutocomplete
							apiKey="AIzaSyDQXEJEdPF6r1WQMhj9rWW03oyV39kh0Dg"
							selectProps={{
								placeholder: "Nhập vị trí cần chọn",
								isClearable: true,
								onChange: onChangeAddress,
							}}
						/>
					}
				</div>
				<MapContainer
					ref={leafletMap}
					center={[10.762622, 106.660172]}
					zoom={13}
					scrollWheelZoom={true}
					style={{ height: "100%", width: "100%", zIndex: 1 }}
					zoomControl={false}
				>
					<ZoomControl position={"bottomright"} />
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

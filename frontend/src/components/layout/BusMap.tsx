import { MapContainer, Marker, Polyline, Popup, TileLayer, ZoomControl } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import React, { useEffect, useRef, useState } from "react";
import { GetServerSideProps, NextPage } from "next";
import { LatLngTuple, Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";
import GooglePlacesAutocomplete, { geocodeByPlaceId } from "react-google-places-autocomplete";
import { Tabs } from "antd";
import RouteInfoView from "./RouteInfoView";
import { debounce } from "lodash";
import Map, { IMapRef } from "~/src/components/Map";
import { useMapControlStore } from "~/src/zustand/MapControlStore";
import SearchMap from "~/src/components/layout/SearchMap";

const SIDE_BAR_WIDTH = 23;
const BusMap: NextPage<any> = ({ children }) => {
	const [isOpenSideBar, setIsOpenSideBar] = useState(true);
	const mapRef = useRef<IMapRef>(null);
	const isFindPathMap = useMapControlStore((state) => state.isFindPathMap);

	const onChangeAddress = debounce(async (address) => {
		mapRef.current?.leafletMap?.current?.fitBounds([[10.7756587, 106.7004238]]);
		mapRef.current?.leafletMap?.current?.setZoom(15);
		try {
			const geocodeObj = await geocodeByPlaceId(address?.value?.place_id);
			if (geocodeObj !== null) {
				let lng = geocodeObj[0]?.geometry?.location?.lng();
				let lat = geocodeObj[0]?.geometry?.location?.lat();
				if (lng && lat) {
					mapRef.current?.setFocusPoints?.([
						{
							pos: [lat, lng],
							type: "CurrentSearch",
							detail: {
								name: address?.label,
							},
						},
					]);
					const ZOOM = 15;
					mapRef.current?.leafletMap?.current?.flyTo([lat, lng], ZOOM);
				}
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
						setTimeout(() => mapRef.current?.leafletMap?.current?.invalidateSize(), 500);
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
				{!isFindPathMap && (
					<div
						style={{
							position: "absolute",
							zIndex: 3,
							width: "40%",
							left: "30px",
							top: "10px",
						}}
					>
						<GooglePlacesAutocomplete
							apiKey={process.env.NEXT_PUBLIC_GG_PLACE_API_KEY}
							debounce={300}
							minLengthAutocomplete={3}
							apiOptions={{
								language: "VN",
								region: "VN",
							}}
							selectProps={{
								placeholder: "Nhập vị trí cần chọn",
								isClearable: true,
								onChange: onChangeAddress,
							}}
						/>
					</div>
				)}
				{isFindPathMap ? <SearchMap /> : <Map ref={mapRef} />}
			</div>
		</>
	);
};

export default BusMap;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

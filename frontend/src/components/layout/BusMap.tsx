import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import React, { useEffect, useRef, useState } from "react";
import { GetServerSideProps, NextPage } from "next";
import GooglePlacesAutocomplete, { geocodeByPlaceId } from "react-google-places-autocomplete";
import RouteInfoView from "./RouteInfoView";
import { debounce } from "lodash";
import Map, { IMapRef } from "~/src/components/Map";
import { useMapControlStore } from "~/src/zustand/MapControlStore";
import SearchMap from "~/src/components/layout/SearchMap";
import {
	PathIconBusHeader1,
	PathIconBusHeader2,
	PathIconBusHeader3,
	PathIconBusHeader4,
	PathIconBusHeader5,
	PathIconBusHeader6,
} from "../pages/svg/Path";

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
		<div>
			<div className="absolute inset-x-0 top-0 bg-emerald-500" style={{ height: "8%", width: "100%" }}>
				<div className="flex w-1/5">
					<div className="bg-blue-light shadow-border p-3 w-4 h-4">
						<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512.002 512.002" className=" sm:w-12 sm:h-12 mr-3">
							<path style={{ fill: "#ffad61" }} d={PathIconBusHeader1} />
							<circle style={{ fill: "#666" }} cx="128.5" cy="338.002" r="46.294" />
							<circle style={{ fill: "#666" }} cx="377.299" cy="338.002" r="46.294" />
							<circle style={{ fill: "#f2f2f2" }} cx="128.5" cy="338.002" r="14.814" />
							<circle style={{ fill: "#f2f2f2" }} cx="377.299" cy="338.002" r="14.814" />
							<path style={{ fill: "#f2f2f2" }} d={PathIconBusHeader2} />
							<path style={{ fill: "#73c1dd" }} d={PathIconBusHeader3} />
							<path style={{ fill: "#f2f2f2" }} d="M221.792 203.209h77.698V339.43h-77.698z" />
							<path style={{ fill: "#4d3d36" }} d={PathIconBusHeader4} />
							<path style={{ fill: "#4d3d36" }} d={PathIconBusHeader5} />
							<path style={{ fill: "#4d3d36" }} d={PathIconBusHeader6} />
						</svg>
					</div>
					<div className="bg-blue-light shadow-border ml-12 mt-4">
						<p className="text-3xl text-white">BusMap</p>
					</div>
				</div>
			</div>
			<div className="absolute inset-x-0 bottom-0" style={{ height: "92%" }}>
				<div
					className="border bg-white-800 flex items-center absolute duration-500 -left-0"
					style={{
						width: isOpenSideBar ? `${SIDE_BAR_WIDTH}%` : 0,
						// height: "95%",
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
					className="h-screen absolute bottom-0 right-0 duration-500"
					style={{
						width: isOpenSideBar ? `${100 - SIDE_BAR_WIDTH}%` : "100%",
						height: "100%",
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
			</div>
		</div>
	);
};

export default BusMap;
export const getServerSideProps: GetServerSideProps = async ({ req, res }) => {
	return {
		props: {},
	};
};

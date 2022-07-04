import React, { useRef } from "react";
import { MapContainer, Marker, Polyline, Popup, TileLayer, ZoomControl } from "react-leaflet";
import L, { Map as LeafletMap } from "leaflet";
import { RoutePathDetailDto, useMapControlStore } from "~/src/zustand/MapControlStore";
import _, { isEmpty } from "lodash";
import { urlBusStop } from "../pages/svg/UrlImage";
import { useMapControlStoreV2 } from "~/src/zustand/MapControlStoreV2";
import shallow from "zustand/shallow";

const iconBusStop = L.icon({
	iconUrl: urlBusStop,
	iconSize: [40, 50],
});
const SearchMap = () => {
	const leafletMap = useRef<LeafletMap>(null);
	const { positions, stops, destination, resultPositions } = useMapControlStoreV2(
		({ stops, positions, destination, resultPositions }) => ({ positions, stops, destination, resultPositions }),
		shallow
	);
	console.log("resultPositions", resultPositions);
	const renderRoutes = () => {
		return resultPositions.map(({ item1, item2, item3 }, key) => {
			const extraProps =
				item3 === "Switch"
					? {
							dashArray: [20, 20],
							dashOffset: "5",
					  }
					: {};
			return <Polyline key={key} positions={[item1, item2]} pathOptions={{ color: "purple" }} {...extraProps} />;
		});
	};

	const renderStops = () => {
		const markers = stops.map((stop, idx) => {
			return (
				<Marker
					position={[stop.lat || _.get(stop, "Lat"), stop.lng || _.get(stop, "Lng")]}
					key={stop.addressNo}
					draggable={false}
					icon={iconBusStop}
				>
					<Popup>
						<div className={"w-full h-full"}>
							<h4 className="font-bold">{stop.name}</h4>
							<h3>{stop.addressNo}</h3>
						</div>
					</Popup>
				</Marker>
			);
		});
		destination.forEach((dest) => {
			markers.push(
				<Marker position={[dest.position.lat, dest.position.lng]} key={dest.address} draggable={false}>
					<Popup>
						<div className={"w-full h-full"}>
							<h4 className="font-bold">{dest.title}</h4>
							<h3>{dest.address}</h3>
						</div>
					</Popup>
				</Marker>
			);
		});
		return markers;
	};
	return (
		<MapContainer
			ref={leafletMap}
			center={[10.7756587, 106.7004238]}
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
			{renderRoutes()}
			{renderStops()}
		</MapContainer>
	);
};

export default SearchMap;

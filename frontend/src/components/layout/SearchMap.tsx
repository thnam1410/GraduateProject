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
	const { positions, stops } = useMapControlStoreV2(({ stops, positions }) => ({ positions, stops }), shallow);

	const renderRoutes = () => {
		return (
			<Polyline
				positions={positions.map((x) => [x.lat || _.get(x, "Lat"), x.lng || _.get(x, "Lng")])}
				pathOptions={{ color: "purple" }}
			/>
		);
	};

	const renderStops = () => {
		return stops.map((stop) => {
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

import React, { useRef } from "react";
import { MapContainer, Marker, Polyline, Popup, TileLayer, ZoomControl } from "react-leaflet";
import L, { Map as LeafletMap } from "leaflet";
import { RoutePathDetailDto, useMapControlStore } from "~/src/zustand/MapControlStore";
import { isEmpty } from "lodash";
import { urlBusStop } from "../pages/svg/UrlImage";

const iconBusStop = L.icon({
	iconUrl: urlBusStop,
	iconSize: [40, 50],
});
const SearchMap = () => {
	const leafletMap = useRef<LeafletMap>(null);
	const routePaths = useMapControlStore((state) => state.routePaths);



	const renderRoutes = () => {
		if (!routePaths || isEmpty(routePaths)) return null;
		return Object.keys(routePaths.paths).map((key) => {
			const item = routePaths.paths[parseInt(key)] as RoutePathDetailDto;
			let extraProps = {};
			if (item.isSwitch) {
				extraProps = {
					dashArray: [20, 20],
					dashOffset: "5",
				};
			}
			return <Polyline key={key} positions={item.positions} pathOptions={{ color: "purple" }} {...extraProps} />;
		});
	};

	const renderStops = () => {
		return (routePaths?.stops || []).map((stop) => {
			return (
				<Marker position={[stop.lat, stop.lng]} key={stop.addressNo} draggable={false} icon={iconBusStop}>
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

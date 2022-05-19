import React, { useRef } from "react";
import { MapContainer, Polyline, TileLayer, ZoomControl } from "react-leaflet";
import { Map as LeafletMap } from "leaflet";
import { RoutePathDetailDto, RoutePathDto, useMapControlStore } from "~/src/zustand/MapControlStore";
import shallow from "zustand/shallow";
import { isEmpty } from "lodash";

const SearchMap = () => {
	const leafletMap = useRef<LeafletMap>(null);
	const routePaths = useMapControlStore((state) => state.routePaths);

	const renderRoutes = () => {
		if (!routePaths || isEmpty(routePaths)) return null;
		console.log('routePaths',routePaths)
		return Object.keys(routePaths).map((key) => {
			const item = routePaths[parseInt(key)] as RoutePathDetailDto;
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
		</MapContainer>
	);
};

export default SearchMap;

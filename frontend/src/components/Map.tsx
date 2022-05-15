import React, { forwardRef, useEffect, useImperativeHandle, useRef, useState } from "react";
import { MapContainer, Marker, Polyline, Popup, TileLayer, ZoomControl } from "react-leaflet";
import { LatLngBounds, LatLngTuple, Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";

interface IProps {}

export interface IMapRef {
	leafletMap: React.RefObject<LeafletMap>;
	setFocusPoints: React.Dispatch<React.SetStateAction<CustomLatLngTuble[] | null>>;
}
interface CustomLatLngTuble {
	pos: [number, number];
	type: "CurrentSearch" | "SubStop";
}

const Map = forwardRef<any, IProps>((props, ref) => {
	const leafletMap = useRef<LeafletMap>(null);
	const polyLineRef = useRef<LeafletPolyline>(null);
	const [positions, setPosition] = useState<any[]>([]);
	const [focusPoints, setFocusPoints] = useState<CustomLatLngTuble[] | null>(null);

	useEffect(() => {
		if (focusPoints) fetchBusStopNearby();
	}, [focusPoints]);

	const fetchBusStopNearby = () => {
		//TODO: Fetch nearby bustop in 1km distance
	};

	const renderMarkersBusStopNearby = () => {
		return focusPoints?.map((point) => {
			switch (point.type) {
				case "CurrentSearch":
					return <Marker position={point.pos} draggable={false} />;
				case "SubStop":
					//TODO: Change marker icon
					return (
						<Marker position={point.pos} draggable={false}>
							<Popup>Stops</Popup>
						</Marker>
					);
			}
		});
	};

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

	useImperativeHandle(ref, () => ({
		leafletMap,
		setFocusPoints,
	}));

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
			{renderMarkers()}
			{renderMarkersBusStopNearby()}
			{renderRoutes()}
		</MapContainer>
	);
});
Map.displayName = "Map";
export default Map;

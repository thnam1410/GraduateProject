import React, { forwardRef, useEffect, useImperativeHandle, useRef, useState } from "react";
import { MapContainer, Marker, Polyline, Popup, TileLayer, ZoomControl } from "react-leaflet";
import { LatLngBounds, LatLngTuple, Map as LeafletMap, Polyline as LeafletPolyline } from "leaflet";
import axios from "axios";
import L from "leaflet";
import { ApiUtil, BASE_API_PATH } from "~/src/utils/ApiUtil";
import { isEmpty } from "lodash";
import { useStore } from "~/src/zustand/store";
import { urlBusStop } from "./pages/svg/UrlImage";

interface IProps {}

export interface IMapRef {
	leafletMap: React.RefObject<LeafletMap>;
	setFocusPoints: React.Dispatch<React.SetStateAction<CustomLatLngTuble[] | null>>;
}

interface CustomLatLngTuble {
	pos: [number, number];
	type: "CurrentSearch" | "SubStop";
	detail?: PointDetail;
}

interface CustomBusStopLatLngTuble {
	pos: [number, number];
	name?: string;
}

interface PointDetail {
	name?: string;
	routes?: string;
	address?: string;
	code?: string;
}

interface Stop {
	lat: number;
	lng: number;
	name: string;
	address: string;
	routes: string;
	code: string;
}

const GetIconBusStop =  (urlImg:string) =>{
	return L.icon({
		iconUrl: urlImg,
		iconSize:[40,50],
		// iconAnchor:[22,94],
	})
}

const Map = forwardRef<any, IProps>((props, ref) => {
	const leafletMap = useRef<LeafletMap>(null);
	const polyLineRef = useRef<LeafletPolyline>(null);
	const positions: LatLngTuple[] = useStore((state) => state.positions);
	const positionsBusStop: CustomBusStopLatLngTuble[] = useStore((state) => state.positionsBusStop);
	const [focusPoints, setFocusPoints] = useState<CustomLatLngTuble[]>([]);

	useEffect(() => {
		if (focusPoints) fetchBusStopNearby();
	}, [focusPoints]);

	useEffect(() => {
		if (leafletMap.current && !isEmpty(positions)) {
			leafletMap.current?.fitBounds(positions);
		}
	}, [positions]);

	const fetchBusStopNearby = async () => {
		if (focusPoints && focusPoints.length === 1) {
			const response = await ApiUtil.Axios.get(BASE_API_PATH + "/route/get-bus-stop-nearby", {
				params: {
					lat: focusPoints[0].pos[0],
					lng: focusPoints[0].pos[1],
				},
			});
			if (response?.data?.success) {
				const results = (response?.data?.result || []) as Stop[];
				if (results.length) {
					const nextState = results.map((stop) => ({
						pos: [stop.lat, stop.lng],
						type: "SubStop",
						detail: {
							name: stop?.name,
							address: stop?.address,
							routes: stop?.routes,
							code: stop?.code,
						},
					})) as CustomLatLngTuble[];
					setFocusPoints((prev) => [...prev, ...nextState]);
				}
			} else {
				console.log("err", response?.data);
			}
		}
	};

	const renderMarkersBusStopNearby = () => {
		return focusPoints?.map((point) => {
			switch (point.type) {
				case "CurrentSearch":
					return (
						<Marker position={point.pos} draggable={false}>
							<Popup>
								<div className={"w-full h-full"}>
									<h4 className="font-bold">{point?.detail?.name}</h4>
								</div>
							</Popup>
						</Marker>
					);
				case "SubStop":
					return (
						<Marker position={point.pos} draggable={false}>
							<Popup>
								<h3 className="font-bold">
									{`${isEmpty(point?.detail?.code) ? "" : `[${point?.detail?.code}]`} 
								    ${point?.detail?.name}`}
								</h3>
								<p>{point?.detail?.address}</p>
								<br />
								<p>Các tuyến đi qua: {point?.detail?.routes}</p>
							</Popup>
						</Marker>
					);
			}
		});
	};

	const renderMarkers = () => {
		return (
			<>
				{positions?.map((position, idx) => {
					switch (idx) {
						case 0:
							return (
								<Marker position={position} draggable={false} icon={GetIconBusStop(urlBusStop)}>
									<Popup>Start</Popup>
								</Marker>
							);
						case positions.length - 1:
							return (
								<Marker position={position} draggable={false} icon={GetIconBusStop(urlBusStop)}>
									<Popup>End</Popup>
								</Marker>
							);
					}
				})}
			</>
		);
	};
	const renderMarkerBustop = () => {
		return positionsBusStop?.map((point: any) => {
			return (
				<>
					<Marker position={point.pos} draggable={false}  icon={GetIconBusStop(urlBusStop)}>
						<Popup>
							<div className={"w-full h-full"}>
								<h4 className="font-bold">{point?.name}</h4>
							</div>
						</Popup>
					</Marker>
				</>
			);
		});
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
			{/* {renderMarkers()} */}
			{renderMarkersBusStopNearby()}
			{renderMarkerBustop()}
			{renderRoutes()}
		</MapContainer>
	);
});
Map.displayName = "Map";
export default Map;

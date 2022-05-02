import L from "leaflet";
import { createControlComponent } from "@react-leaflet/core";
import "leaflet-routing-machine";
import "lrm-graphhopper";
import "leaflet-routing-machine/dist/leaflet-routing-machine.css";

var greenIcon = new L.Icon({
	iconUrl: "https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png",
	shadowUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.3.4/images/marker-shadow.png",
	iconSize: [25, 41],
	iconAnchor: [12, 41],
	popupAnchor: [1, -34],
	shadowSize: [41, 41],
});

const createRoutineMachineLayer = () => {
	const planOptions = {
		addWaypoints: false,
		draggableWaypoints: false,
	};
	// const plan = new L.Routing.Plan(
	// 	[
	// 		L.latLng(10.75104046, 106.65258026),
	// 		L.latLng(10.75094986, 106.65097046),
	// 		L.latLng(10.75100994, 106.65048218),
	// 		L.latLng(10.7514801, 106.65052795),
	// 		L.latLng(10.75234032, 106.65061951),
	// 		L.latLng(10.75352001, 106.65074158),
	// 	],
	// 	planOptions
	// );
	// const line = new L.Routing.Line([L.latLng(10.75104046, 106.65258026)],{})
	const instance = L.Routing.control({
		waypoints: [
			L.latLng(10.75104046, 106.65258026),
			// L.latLng(10.75094986, 106.65097046),
			// L.latLng(10.75100994, 106.65048218),
			// L.latLng(10.7514801, 106.65052795),
			// L.latLng(10.75234032, 106.65061951),
			L.latLng(10.75352001, 106.65074158),
		],
		// plan,
		lineOptions: {
			styles: [
				{
					color: "blue",
					opacity: 0.6,
					weight: 4,
				},
			],
		},
		show: false,
		routeWhileDragging:true,
		reverseWaypoints:true,
		showAlternatives:true,
		createMarker: function (i, wp, nWps) {
			switch (i) {
				case 0:
					return L.marker(wp.latLng, {}).bindPopup("<b>" + "Điểm đi" + "</b>");
				case nWps - 1:
					return L.marker(wp.latLng, {
						icon: greenIcon,
					}).bindPopup("<b>" + "Điểm đến" + "</b>");
				default:
					return L.marker(wp.latLng, {}).bindPopup("a");
			}
		},
	});
	return instance;
};

const RoutingMachine = createControlComponent(createRoutineMachineLayer);

export default RoutingMachine;

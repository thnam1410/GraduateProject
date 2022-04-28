import L from "leaflet";
import { createControlComponent } from "@react-leaflet/core";
import "leaflet-routing-machine";
import "lrm-graphhopper";
import "leaflet-routing-machine/dist/leaflet-routing-machine.css";

var greenIcon = new L.Icon({
	iconUrl:
	  "https://cdn.rawgit.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png",
	shadowUrl:
	  "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.3.4/images/marker-shadow.png",
	iconSize: [25, 41],
	iconAnchor: [12, 41],
	popupAnchor: [1, -34],
	shadowSize: [41, 41],
  });

const createRoutineMachineLayer = () => {
	// const { map, dataLng, dataLet } = this.props;
	// let dataWayPoint = [];
    // for (let i = 0; i < _.get(dataLng, "length"); i++) {
    //   dataWayPoint.push(L.latLng(dataLet[i], dataLng[i]));
    // }
	const instance = L.Routing.control({
		waypoints: [
			  L.latLng(10.75125313, 106.652565),
			  L.latLng(10.80402565, 106.65221405),

		],
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
		addWaypoints: false,
		routeWhileDragging: true,
		draggableWaypoints: true,
		fitSelectedRoutes: true,
		showAlternatives: false,
		createMarker: function (i, wp, nWps) {
			switch (i) {
			  case 0:
				return L.marker(wp.latLng, {
				  draggable: true,
				}).bindPopup("<b>" + "Điểm đi" + "</b>");
			  case nWps - 1:
				return L.marker(wp.latLng, {
				  icon: greenIcon,
				  draggable: true,
				}).bindPopup("<b>" + "Điểm đến" + "</b>");
			  default:
				return null;
			}
		  },
	});

	return instance;
};

const RoutingMachine = createControlComponent(createRoutineMachineLayer);

export default RoutingMachine;

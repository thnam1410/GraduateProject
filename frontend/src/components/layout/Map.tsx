import { MapContainer, TileLayer, Marker, Popup } from "react-leaflet";
import "leaflet/dist/leaflet.css";
import "leaflet-defaulticon-compatibility/dist/leaflet-defaulticon-compatibility.css";
import "leaflet-defaulticon-compatibility";
import useBrowser from "~/src/hooks/useBrowser";

const Map: React.FC = () => {
	const isMounted = useBrowser();
	console.log("isMounted", isMounted);
	if (!isMounted) return null;

	return (
		<>
			<MapContainer center={[10.762622, 106.660172]} zoom={14} scrollWheelZoom={true} style={{ height: "100%", width: "100%" }}>
				<TileLayer
					url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
					attribution='&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors'
				/>
			</MapContainer>
		</>
	);
};

export default Map;

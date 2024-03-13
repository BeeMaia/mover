import { LatLngExpression } from "leaflet";
import "leaflet/dist/leaflet.css";
import { MapContainer, Polyline, TileLayer } from "react-leaflet";

interface GpxMapProps {
    positions: LatLngExpression[];
}

const GpxMap: React.FC<GpxMapProps> = ({ positions }) => {
    return (
        <MapContainer center={positions[0]} zoom={9} scrollWheelZoom={false}>
            <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
            <Polyline
                pathOptions={{ fillColor: "red", color: "blue" }}
                positions={positions}
            />
        </MapContainer>
    );
};

export default GpxMap;

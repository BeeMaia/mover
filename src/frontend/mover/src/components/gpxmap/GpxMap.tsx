import { LatLngExpression } from "leaflet";
import "leaflet/dist/leaflet.css";
import { MapContainer, Polyline, TileLayer } from "react-leaflet";

interface GpxMapProps {
    positions: LatLngExpression[];
}

const GpxMap: React.FC<GpxMapProps> = ({ positions }) => {
    const style = { height: "400px" };
    const attribution =
        '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors';

    return (
        <MapContainer
            center={positions[0]}
            zoom={10}
            style={style}
            scrollWheelZoom={false}
        >
            <Polyline
                pathOptions={{ color: "red", opacity: 1, stroke: true }}
                positions={positions}
            />
            <TileLayer
                url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                minZoom={0}
                maxZoom={20}
                attribution={attribution}
            />
        </MapContainer>
    );
};

export default GpxMap;

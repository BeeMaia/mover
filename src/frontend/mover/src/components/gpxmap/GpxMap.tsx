import { LatLngExpression } from "leaflet";
import "leaflet/dist/leaflet.css";
import { MapContainer, Polyline, TileLayer } from "react-leaflet";

interface GpxMapProps {
    positions: LatLngExpression[];
}

const GpxMap: React.FC<GpxMapProps> = ({ positions }) => {
    const style = { height: "400px" };
    const attribution =
        '&copy; <a href="https://www.stadiamaps.com/" target="_blank">Stadia Maps</a> &copy; <a href="https://openmaptiles.org/" target="_blank">OpenMapTiles</a> &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors';

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
                url="https://tiles.stadiamaps.com/tiles/osm_bright/{z}/{x}/{y}{r}.png"
                minZoom={0}
                maxZoom={20}
            />
        </MapContainer>
    );
};

export default GpxMap;

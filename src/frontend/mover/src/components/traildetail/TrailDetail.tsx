import "./traildetail.scss";
import { activityTypes } from "../../data";
import { activityFull } from "../../models/activity";
import { statData } from "../../models/statData";
import StatsChart from "../chart/Chart";
import GpxMap from "../gpxmap/GpxMap";
import { LatLngExpression } from "leaflet";

interface TrailDetailProps {
    a: activityFull | undefined;
}

export const TrailDetail: React.FC<TrailDetailProps> = ({ a }) => {
    if (a === undefined) return;

    const formattedDateTime = new Intl.DateTimeFormat("it-IT", {
        year: "numeric",
        month: "2-digit",
        day: "2-digit",
        hour: "2-digit",
        minute: "2-digit",
        second: "2-digit",
    }).format(a.timestamp * 1000);

    const dateObject = new Date(0);
    dateObject.setSeconds(a.tTime);
    const formattedTime = dateObject.toISOString().substr(11, 5); // Estrae solo HH:mm

    // Da rivedere mappatura, valido esclusivamente per meetup
    const activityType = activityTypes.get(a.activityType.toLowerCase());

    const hrData: statData[] = a.points.map((x) => ({
        time: x.ts,
        value: x.hr,
    }));
    const eleData: statData[] = a.points.map((x) => ({
        time: x.ts,
        value: x.ele,
    }));
    const cadData: statData[] = a.points.map((x) => ({
        time: x.ts,
        value: x.c,
    }));
    const sData: statData[] = a.points.map((x) => ({
        time: x.ts,
        value: x.s,
    }));
    const tempData: statData[] = a.points.map((x) => ({
        time: x.ts,
        value: x.temp,
    }));

    const positions: LatLngExpression[] = [
        [40.689818841705, -74.04511194542516],
        [40.75853187779803, -73.98495720388513],
        [40.86151538060051, -74.06201170384256],
        [40.80981015620906, -74.03656769139772],
        [40.80721155324825, -74.04274750092904],
        [40.78901848327006, -74.081199649124],
        [40.764319913561216, -74.08840942691056],
        [40.749756455072884, -74.09493255919364],
        [40.74793579843903, -74.07673645335137],
        [40.675849802727335, -74.19758606169779],
        [40.60394644123212, -74.05991363796608],
        [40.6495463256113, -73.96000671720954],
    ];

    return (
        <article className="trail-card">
            <div className="trail-card-header">
                <span className="datetime">{formattedDateTime}</span>
            </div>
            <div className="trail-card-body">
                <div className="title">
                    <img
                        height="25"
                        alt="Attività"
                        aria-hidden="true"
                        className="activity-icon"
                        src={activityType.icon}
                    />
                    <h2>{activityType.title}</h2>
                </div>
                <div className="trail-card-detail">
                    <div className="stats">
                        <div className="name">Distanza</div>
                        <div className="value">{a.tDistance.toFixed(2)} km</div>
                    </div>
                    <div className="stats">
                        <div className="name">Dislivello positivo</div>
                        <div className="value">{a.tPDrop.toFixed(0)} m</div>
                    </div>
                    <div className="stats">
                        <div className="name">Tempo</div>
                        <div className="value">{formattedTime}</div>
                    </div>
                </div>
            </div>
            <GpxMap positions={positions} />
            <StatsChart items={eleData} color="#50b012" title="Quota" />
            <StatsChart items={sData} color="#11a9ed" title="Velocità" />
            <StatsChart
                items={hrData}
                color="#ff0035"
                title="Frequenza cardiaca"
            />
            <StatsChart items={cadData} color="#ed7e00" title="Cadenza" />
            <StatsChart items={tempData} color="#888" title="Temperatura" />
        </article>
    );
};

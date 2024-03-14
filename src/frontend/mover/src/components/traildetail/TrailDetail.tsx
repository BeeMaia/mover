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
    const activityType = activityTypes.get(a.activityType.toLowerCase()) ?? {
        title: "Attività generica",
        icon: "/generic.svg",
    };

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

    const positions: LatLngExpression[] = a.positions.map((x) => [
        x.lat,
        x.lng,
    ]);

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
            <div className="map-container">
                <GpxMap positions={positions} />
            </div>
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

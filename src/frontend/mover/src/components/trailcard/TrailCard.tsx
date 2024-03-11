import "./trailcard.scss";
import { Link } from "react-router-dom";
import { activityTypes } from "../../data";
import { activity } from "../../models/activity";

interface TrailCardProps {
    a: activity;
}

export const TrailCard: React.FC<TrailCardProps> = ({ a }) => {
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
            <div className="trail-card-footer">
                <Link to={`/activity/${a.idRaw}`} className="action">
                    Vedi percorso
                </Link>
            </div>
        </article>
    );
};

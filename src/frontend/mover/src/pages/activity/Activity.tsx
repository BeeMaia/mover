import { useState, useEffect } from "react";
import "./activity.scss";
import { useParams } from "react-router-dom";
import { TrailDetail } from "../../components/traildetail/TrailDetail";
import { activityFull } from "../../models/activity";
import ProgressBar from "../../components/progressbar/ProgressBar";

export const Activity = () => {
    const { id } = useParams();
    const [data, setData] = useState<activityFull>();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `${import.meta.env.VITE_APP_GATEWAY_URL}/s/v1/stats/${id}`
                );
                const result: activityFull = await response.json();
                setData(result);
                setLoading(false);
            } catch (error) {
                console.error("Error fetching data:", error);
            }
        };

        fetchData();
    }, []);

    return (
        <div className="activity-container">
            {loading ? (
                <ProgressBar />
            ) : (
                <div className="activity">
                    <div className="box">
                        <TrailDetail a={data} />
                    </div>
                </div>
            )}
        </div>
    );
};

import { useState, useEffect } from "react";
import "./activity.scss";
import { useParams } from "react-router-dom";
import { TrailDetail } from "../../components/traildetail/TrailDetail";
import { activityFull } from "../../models/activity";

export const Activity = () => {
    const { id } = useParams();
    const [data, setData] = useState<activityFull>();
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `http://localhost:10000/s/v1/stats/${id}`
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
                <p>Loading...</p>
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

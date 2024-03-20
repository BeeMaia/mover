import { useState, useEffect } from "react";
import "./activity.scss";
import { useNavigate, useParams } from "react-router-dom";
import { TrailDetail } from "../../components/traildetail/TrailDetail";
import { activityFull } from "../../models/activity";
import ProgressBar from "../../components/progressbar/ProgressBar";

export const Activity = () => {
    const { id } = useParams();
    const [data, setData] = useState<activityFull>();
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        var user = JSON.parse(localStorage.getItem("user")!);
        const headers = { Authorization: `Bearer ${user.token.result}` };

        const fetchData = async () => {
            try {
                const response = await fetch(
                    `${import.meta.env.VITE_APP_GATEWAY_URL}/s/v1/stats/${id}`,
                    { headers }
                );
                if (response.ok) {
                    const result: activityFull = await response.json();
                    setData(result);
                    setLoading(false);
                } else {
                    localStorage.removeItem("user");
                    navigate("/login");
                }
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

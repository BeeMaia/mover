import { useState, useEffect } from "react";
import { TrailCard } from "../../components/trailcard/TrailCard";
import "./home.scss";
import { activity } from "../../models/activity";
import FileUpload from "../../components/fileupload/FileUpload";
import ProgressBar from "../../components/progressbar/ProgressBar";
import { useNavigate } from "react-router-dom";
export const Home = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);
    const navigate = useNavigate();

    useEffect(() => {
        var user = JSON.parse(localStorage.getItem("user")!);
        const headers = { Authorization: `Bearer ${user.token.result}` };
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `${import.meta.env.VITE_APP_GATEWAY_URL}/s/v1/stats`,
                    { headers }
                );
                if (response.ok) {
                    const result = await response.json();
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
        <div className="home-container">
            {loading ? (
                <ProgressBar />
            ) : (
                <div className="home">
                    <div className="box-upload">
                        <FileUpload />
                    </div>
                    {data.map((item: activity) => (
                        <div className="box" key={item.idRaw}>
                            <TrailCard a={item} />
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

import { useState, useEffect } from "react";
import { TrailCard } from "../../components/trailcard/TrailCard";
import "./home.scss";
import { activity } from "../../models/activity";
import FileUpload from "../../components/fileupload/FileUpload";
import ProgressBar from "../../components/progressbar/ProgressBar";
export const Home = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `${import.meta.env.VITE_APP_GATEWAY_URL}/s/v1/stats`
                );
                const result = await response.json();
                setData(result);
                setLoading(false);
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

import { Line } from "react-chartjs-2";
import { statData } from "../../models/statData";
import { CategoryScale, Chart, Colors } from "chart.js/auto";

Chart.register(CategoryScale);
Chart.register(Colors);

interface StatsChartProps {
    items: statData[];
    color: string;
    title: string;
}

const StatsChart: React.FC<StatsChartProps> = ({ items, color, title }) => {
    const chartData = {
        labels: items.map((entry) => entry.time),
        datasets: [
            {
                data: items.map((entry) => entry.value),
                pointRadius: 0,
                fill: true,
                backgroundColor: color,
            },
        ],
    };

    const options = {
        responsive: true,
        aspectRatio: 8,
        plugins: {
            legend: {
                display: false,
                position: "top" as const,
            },
            title: {
                text: title,
                display: true,
                align: "start" as const,
            },
        },
        scales: {
            x: {
                grid: {
                    display: false,
                },
                display: false,
            },
            y: {
                grid: {
                    display: false,
                },
            },
        },
    };

    return <Line data={chartData} options={options} />;
};

export default StatsChart;

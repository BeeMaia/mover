import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App.tsx";

console.log(import.meta.env.VITE_APP_TITLE);
ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(
    <React.StrictMode>
        <App />
    </React.StrictMode>
);

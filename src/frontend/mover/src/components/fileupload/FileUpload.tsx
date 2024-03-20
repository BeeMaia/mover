import { toast } from "react-toastify";
import "./fileupload.scss";
import React, { ChangeEvent, useRef } from "react";

const FileUpload: React.FC = () => {
    const fileInputRef = useRef<HTMLInputElement>(null);

    const handleButtonClick = () => {
        if (fileInputRef.current) {
            fileInputRef.current.click(); // Trigger file input click
        }
    };

    const handleFileChange = async (e: ChangeEvent<HTMLInputElement>) => {
        if (e.target.files && e.target.files.length > 0) {
            const selectedFile = e.target.files[0];

            const formData = new FormData();
            formData.append("file", selectedFile);
            try {
                var user = JSON.parse(localStorage.getItem("user")!);
                const headers = {
                    Authorization: `Bearer ${user.token.result}`,
                };
                const response = await fetch(
                    `${
                        import.meta.env.VITE_APP_GATEWAY_URL
                    }/u/v1/uploader/upload`,
                    {
                        method: "POST",
                        body: formData,
                        headers,
                    }
                );
                if (response.ok) {
                    toast.success("File caricato con successo");
                } else {
                    toast.error("Il file non è stato caricato");
                    console.error(
                        "Failed to upload file:",
                        response.statusText
                    );
                }
            } catch (error) {
                toast.error("Errore durante il caricamento del file");
                console.error("Error uploading file:", error);
            }
        }
    };

    return (
        <div className="file-uploader">
            <h2>Le mie attivit&agrave;</h2>
            <input
                type="file"
                onChange={handleFileChange}
                accept=".fit,.gpx"
                ref={fileInputRef}
            />
            <button className="custom-button" onClick={handleButtonClick}>
                <img
                    height="16"
                    alt="Carica attività"
                    aria-hidden="true"
                    src="/upload.svg"
                    title="Carica attività"
                />
            </button>
        </div>
    );
};

export default FileUpload;

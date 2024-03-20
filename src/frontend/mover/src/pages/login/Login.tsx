import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./login.scss";
import { toast } from "react-toastify";

interface LoginProps {
    setLoggedIn: (value: boolean) => void;
    setLoggedUser: (value: string) => void;
}

export const Login: React.FC<LoginProps> = ({ setLoggedIn, setLoggedUser }) => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const login = async () => {
        const response = await fetch(
            `${import.meta.env.VITE_APP_GATEWAY_URL}/a/v1/auth/login`,
            {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password }),
            }
        );
        if (response.ok) {
            var body = await response.json();
            localStorage.setItem(
                "user",
                JSON.stringify({ email, token: body.token })
            );
            setLoggedIn(true);
            setLoggedUser(email);
            navigate("/");
        } else {
            toast.error("Username o Password errati");
        }
    };

    const navigate = useNavigate();

    return (
        <div className="loginContainer">
            <div className="login">
                <div className="wrapper">
                    <div className="title">
                        <h2>Accedi</h2>
                    </div>
                    <div className="line"></div>
                    <div className="left">
                        <input
                            value={email}
                            type="text"
                            placeholder="Username"
                            onChange={(ev) => setEmail(ev.target.value)}
                        />
                        <input
                            value={password}
                            type="password"
                            placeholder="Password"
                            onChange={(ev) => setPassword(ev.target.value)}
                        />
                        <button
                            className="submit"
                            type="button"
                            onClick={login}
                        >
                            Accedi
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};

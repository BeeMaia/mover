import {
    createBrowserRouter,
    RouterProvider,
    Outlet,
    Navigate,
} from "react-router-dom";
import { Home } from "./pages/home/Home";
import { Activity } from "./pages/activity/Activity";
import { NavBar } from "./components/navbar/NavBar";
import { Footer } from "./components/footer/Footer";
import { Login } from "./pages/login/Login";
import "./styles/global.scss";
import { useState } from "react";

function App() {
    const [loggedIn, setLoggedIn] = useState(() => {
        const userStorage = localStorage.getItem("user");
        return userStorage !== null;
    });
    const [email, setEmail] = useState(() => {
        const userStorage = localStorage.getItem("user");
        if (userStorage) {
            const user = JSON.parse(userStorage);

            return user.email;
        }
        return "";
    });

    const Layout = () => {
        return loggedIn ? (
            <div className="main">
                <NavBar username={email} />
                <div className="container">
                    <div className="contentContainer">
                        <Outlet />
                    </div>
                </div>
                <Footer />
            </div>
        ) : (
            <Navigate to="/login" replace />
        );
    };

    const router = createBrowserRouter([
        {
            path: "/",
            element: <Layout />,
            children: [
                { path: "/", element: <Home /> },
                { path: "/about", element: <div>About</div> },
                { path: "/activity/:id", element: <Activity /> },
            ],
        },
        {
            path: "/login",
            element: (
                <Login setLoggedIn={setLoggedIn} setLoggedUser={setEmail} />
            ),
        },
    ]);

    return <RouterProvider router={router} />;
}

export default App;

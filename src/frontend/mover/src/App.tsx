import { createBrowserRouter, RouterProvider, Outlet } from "react-router-dom";
import { Home } from "./pages/home/Home";
import { Activity } from "./pages/activity/Activity";
import { NavBar } from "./components/navbar/NavBar";
import { Footer } from "./components/footer/Footer";
import { Login } from "./pages/login/Login";
import "./styles/global.scss";

function App() {
    const Layout = () => {
        return (
            <div className="main">
                <NavBar />
                <div className="container">
                    <div className="contentContainer">
                        <Outlet />
                    </div>
                </div>
                <Footer />
            </div>
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
            element: <Login />,
        },
    ]);

    return <RouterProvider router={router} />;
}

export default App;

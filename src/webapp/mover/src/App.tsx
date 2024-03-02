import { createBrowserRouter, RouterProvider, Outlet } from "react-router-dom";
import { Home } from "./pages/home/Home";
import { NavBar } from "./components/navbar/NavBar";
import { Footer } from "./components/footer/Footer";
import { SideBar } from "./components/sidebar/SideBar";
import { Login } from "./pages/login/Login";
import "./styles/global.scss";

function App() {
  const Layout = () => {
    return (
      <div className="main">
        <NavBar />
        <div className="container">
          <div className="sidebarContainer">
            <SideBar />
          </div>
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

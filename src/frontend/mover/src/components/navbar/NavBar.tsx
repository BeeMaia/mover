import { Link } from "react-router-dom";
import { menu } from "../../data";
import "./navbar.scss";

interface NavBarProps {
    username: string;
}

export const NavBar: React.FC<NavBarProps> = ({ username }) => {
    return (
        <div className="navbar">
            <div className="menu">
                <div className="logo">
                    <h1>mover</h1>
                </div>
                {menu.map((item) => (
                    <div className="item" key={item.id}>
                        {item.listItems.map((listItem) => (
                            <Link
                                to={listItem.url}
                                className="listItem"
                                key={listItem.id}
                            >
                                <img src={listItem.icon} alt="" />
                                <span className="listItemTitle">
                                    {listItem.title}
                                </span>
                            </Link>
                        ))}
                    </div>
                ))}
            </div>
            <div className="icons">
                <div className="notification">
                    <img src="/notifications.svg" alt="" />
                    <span>1</span>
                </div>
                <div className="user">
                    <img src={`/${username}.jpg`} alt="" />
                    <span>{username}</span>
                </div>
                <img src="/settings.svg" alt="" className="icon" />
            </div>
        </div>
    );
};

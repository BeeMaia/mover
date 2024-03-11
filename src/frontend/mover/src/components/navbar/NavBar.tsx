import { Link } from "react-router-dom";
import { menu } from "../../data";
import "./navbar.scss";

export const NavBar = () => {
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
                    <img
                        src="https://yt3.ggpht.com/WMbrNFgi-hg7Asxl0n2yJXgnmDIXkUo_f3ZzR_INlJnttieS1xvwGjOk0k4LikCOHEid0eAe9w=s88-c-k-c0x00ffffff-no-rj"
                        alt=""
                    />
                    <span>Andrea</span>
                </div>
                <img src="/settings.svg" alt="" className="icon" />
            </div>
        </div>
    );
};

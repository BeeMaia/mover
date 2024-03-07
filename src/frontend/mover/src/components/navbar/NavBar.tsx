import "./navbar.scss";

export const NavBar = () => {
  return (
    <div className="navbar">
      <div className="logo">
        <img src="logo.svg" alt="" />
        <span>Mover</span>
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

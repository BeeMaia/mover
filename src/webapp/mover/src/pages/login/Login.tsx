import Google from "../../../public/google.png";
import Facebook from "../../../public/facebook.png";
import "./login.scss";

export const Login = () => {
  const google = () => {};

  const facebook = () => {};

  return (
    <div className="loginContainer">
      <div className="login">
        <div className="wrapper">
          <div className="left">
            <div className="loginButton google" onClick={google}>
              <img src={Google} alt="" className="icon" />
              Google
            </div>
            <div className="loginButton facebook" onClick={facebook}>
              <img src={Facebook} alt="" className="icon" />
              Facebook
            </div>
          </div>
          <div className="center">
            <div className="line" />
            <div className="or">OR</div>
          </div>
          <div className="right">
            <input type="text" placeholder="Username" />
            <input type="text" placeholder="Password" />
            <button className="submit">Login</button>
          </div>
        </div>
      </div>
    </div>
  );
};

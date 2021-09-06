import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux"
import { Link, useHistory } from "react-router-dom";
import { getUser, userService } from "../../../reduxs/user/api/userService";
import LoadingInfinite from "../../../shared/Loading/LoadingCircle/LoadingInfinite";
import "../css/user-infor.css";

export default function ClientInfo({ handleShow, show, userLogined}){
    const user = useSelector(state => state.userCurrent);
    const dispatch = useDispatch();
    const [isShow, setShow] = useState(false);

    const history = useHistory();
    const logoutHanlder = () => {
        history.push("/");
        dispatch(userService.logout());
    };

    useEffect(() => {
        setShow(false);
        if(userLogined?.loggedIn){
            dispatch(getUser());
        }
    }, [dispatch, userLogined])

    return (
        (userLogined?.loggedIn
        && <div className="user-lged">
            
                {(user?.loading && <LoadingInfinite></LoadingInfinite>)
                || <div onClick={() => setShow(!isShow)} className="user-avt" title={user?.name}>
                        <img src={`${user?.photo_url === "" 
                            ? "https://i.imgur.com/t6mJtNE.png" : user?.photo_url }`} alt={user?.name} />
                    </div>}
                
                {isShow && 
                <div className="user-menu">
                    <div className="user-profile">
                        <p className="name">{user?.name}</p>
                        <p className="email">{user?.email}</p>
                    </div>
                    <div className="grid-menu">
                        <div className="item">
                            <Link to="/user/profile"><i className="fas fa-user mr-2"></i>Trang cá nhân</Link>
                            <Link to="/user/password"><i className="fas fa-key"></i>Đổi mật khẩu</Link>
                        </div>
                        <div className="item">
                            <Link to="/user/love"><i className="fas fa-heart"></i>Anime yêu thích</Link>
                        </div>
                    </div>
                    <div className="log-out">
                        <span onClick={() => logoutHanlder()}>Đăng xuất<i className="fas fa-arrow-right ml-2 mr-1"></i></span>
                    </div>
                </div>}
            </div>)
        || <span onClick={handleShow}
                className={`anime-login${show ? ' hidden' : ''}`} title="Đăng nhập"><i className="fas fa-user"></i>
            </span>
    )
}
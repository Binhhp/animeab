import React, { useState } from "react";
import { Link } from "react-router-dom";
import "./nav-bar.css";
import usePosition from "../../hooks/usePosition";
import Search from "../../shared/Search/Search";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { useSelector } from "react-redux";
import FormClient from "../../fetures/Users/component/FormClient";
import Notification from "../../fetures/NotificationHub/component/Notification";
import logo from "../../assets/img/logo.png";

export default function NavBar({ ...props }) {
    
    const [isSearch, setSearch] = useState(false);
    const position = usePosition();

    const animes = useSelector(state => state.animes.data);
    const animesFilter = animes.length > 0 ? animes.filter(x => x.isStatus > 1) : [];
    
    const random = animesFilter.length > 0 ? animesFilter[Math.floor(Math.random() * animesFilter.length)] : {};

    return (
            <div className={`fixed-header ${position > 70 ? "bg-shadow" : ""}`}>
                <div className="container">
                    <div className="mobie_menu">
                        <span onClick={() => props.toggleMenuLeft(props.className)} className="menu-left">
                            <i className="fas fa-bars"></i>
                        </span>
                    </div>
                    {/* logo */}
                    <div className="logo">
                        <Link to="/" title="AnimeAB">
                            <img src={logo} alt="AnimeAB"/>
                        </Link>
                    </div>
                    {/* search */}
                    <div className={isSearch ? "search open" : "search"}>	
                        <Search></Search>
                    </div>
                    {/* random film */}
                    <div className="btn-toggle">
                        <OverlayTrigger 
                            key="contact-fb"
                            placement="bottom"
                            overlay={
                                <Tooltip className="tooltip-title">
                                    Tham gia group facebook
                                </Tooltip>
                            }>
                            <Link to="/" className="contact bg-blue"><i className="fab fa-facebook-f"></i></Link>
                        </OverlayTrigger>
                        <OverlayTrigger 
                            key="contact-tw"
                            placement="bottom"
                            overlay={
                                <Tooltip className="tooltip-title">
                                    Tham gia group twitter
                                </Tooltip>
                            }>
                            <Link to="/" className="contact bg-purple"><i className="fab fa-twitter"></i></Link>
                        </OverlayTrigger>
                        <OverlayTrigger 
                            key="random-anime"
                            placement="bottom"
                            overlay={
                                <Tooltip className="tooltip-title">
                                    Xem anime ngẫu nhiên
                                </Tooltip>
                            }>
                            <Link to={`/xem-phim/${random.key}/${random.isStatus < 3 ? random.linkEnd : random.linkStart}`}  className="random-film">
                                <div className="rd-icon">
                                    <i className="fa fa-random"></i>
                                </div>
                                <div className="rd-title">Random</div>
                            </Link>
                        </OverlayTrigger>
                    </div>
                    {/* mobie search */}
                    <div className="mobie__search" onClick={() => setSearch(!isSearch)}>
                        <i className="fas fa-search"></i>
                    </div>
                    <Notification></Notification>
                    <FormClient></FormClient>
                    {/* menu right */}
                    <span onClick={() => props.toggleMenuRight()} className="toggle-open-close-right">
                        <i className="fas fa-bars"></i>
                    </span>
                </div>
            </div>
     )
}
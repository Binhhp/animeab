import React from "react";
import { Accordion } from "react-bootstrap";
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import "./side-nav.css";

export default function SideNavRight({ ...props }){
    
    const categories = useSelector(state => state.categories.data);
    const collections = useSelector(state => state.collections.data);

    const color = ["#d0e6a5", "#ffdd95", "#fc887b", "#ccabda", "#abccd8", "#d8b2ab"];

    return(
        <div id="ingmarwp-sidenav-right" 
        className={`sidenav-r bg-sidenav border-sidenav-left ${props.isMenuRight ? "sidenav-w-right" : ""}`}>
            <div className="sidenav-margin-top"> 
                <div className="sidenav-nav-container">
                    <div id="nav_menu-2" className="widget_nav_menu mb-3">
                        <div className="menu-sidenav-menu-container">
                            <button className="btn btn-radius btn-sm btn-secondary toggle-sidebar" onClick={() => props.toggleMenuRight(false)}>
                                    Đóng menu&nbsp;<i className="fas fa-angle-right mr-2"></i>
                            </button>
                             <ul className="menu">
                                <li className="menu-item menu-hover">
                                    <Link to="/">
                                        <i className="fas fa-home fa-sm cate__item"></i>Trang chủ
                                    </Link>
                                </li>
                                <li className="menu-item menu-hover">
                                    <Accordion>
                                        <Accordion.Toggle  
                                            className="parent" eventKey="0"  as="a" variant="link">
                                            <i className="fas fa-video fa-sm cate__item"></i>Thể loại
                                        </Accordion.Toggle>
                                        <div className='dropdown-sidenav-btn'><i className='far fa-dot-circle fa-lg'></i></div>
                                        <Accordion.Collapse 
                                                className="sidebar-toggled"
                                                eventKey="0">
                                                    <ul className="sub-menu">
                                                    {
                                                        categories.length > 0 ? categories.map((item, i) => (
                                                            <li key={`side-r-${i}`} className="menu-item menu-hover">
                                                                <Link style={{color: `${color[i % 6]}`}} to={`/${item.key}`}>{item.title}</Link>
                                                            </li>
                                                          ))
                                                           : ""
                                                       }
                                                    </ul>
                                                </Accordion.Collapse>
                                        
                                    </Accordion>
                                </li>
                                <li className="menu-item menu-hover">
                                    <Accordion>
                                        <Accordion.Toggle 
                                        className="parent"
                                         eventKey="1"  as="a" 
                                         variant="link">
                                            <i className="fas fa-trophy cate__item"></i>Bộ sưu tập
                                        </Accordion.Toggle>
                                        <div className='dropdown-sidenav-btn'><i className='far fa-dot-circle fa-lg'></i></div>
                                        <Accordion.Collapse eventKey="1">
                                        <ul className="sub-menu">
                                                {
                                                    collections.length > 0 ? collections.map((item, i) => (
                                                        <li key={item.key} className="menu-item menu-hover">
                                                            <Link style={{color: `${color[i % 6]}`}} to={`/${item.key}`}>{item.title}</Link>
                                                        </li>
                                                    ))
                                                        : ""
                                                }
                                        </ul>
                                    </Accordion.Collapse>
                                    </Accordion>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div> 
            </div> 
        </div>
    )
}
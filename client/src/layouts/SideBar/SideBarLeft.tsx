import "./side-nav.css";
import { Accordion } from "react-bootstrap";
import React from "react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";

export default function SideBarLeft({ isMenuLeft, setMenuLeft }: any) {

    const categories = useSelector((state: any) => state.categories.data);
    const cateIndexes = categories.length > 0 ? categories : [];

    const collections = useSelector((state: any) => state.collections.data);
    const collectIndexes = collections.length > 0 ? collections : [];

    const color = ["#d0e6a5", "#ffdd95", "#fc887b", "#ccabda", "#abccd8", "#d8b2ab"];
    
    return(
        <nav id="sidebar" className={isMenuLeft ? "sidenav open" : "sidenav"}>
            <div className="scrollbar-sidenav" data-simplebar>
                <div className="sidenav-nav-container">
                    <div className="widget_nav_menu mb-3">
                        <div className="menu-sidenav-menu-container">
                            <button 
                                className="btn btn-radius btn-sm btn-secondary toggle-sidebar" 
                                onClick={() => setMenuLeft(false)}>
                                <i className="fas fa-angle-left mr-2"></i>Đóng menu
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
                                            className="parent" eventKey="0"  as="a">
                                            <i className="fas fa-video fa-sm cate__item"></i>Thể loại
                                        </Accordion.Toggle>
                                        <div className='dropdown-sidenav-btn'><i className='far fa-dot-circle fa-lg'></i></div>
                                        <Accordion.Collapse 
                                                className="sidebar-toggled"
                                                eventKey="0">
                                                    <ul className="sub-menu">
                                                    {
                                                        cateIndexes.map((item: any, i: number) => (
                                                            <li key={item.key} className="menu-item menu-hover">
                                                                <Link to={`/anime/${item.key}`} style={{color: `${color[i % 6]}`}}>{item.title}</Link>
                                                            </li>
                                                            ))
                                                      }
                                                    </ul>
                                                </Accordion.Collapse>
                                        
                                    </Accordion>
                                </li>
                                <li className="menu-item menu-hover">
                                    <Accordion>
                                        <Accordion.Toggle 
                                            key="collection"
                                            className="parent"
                                            eventKey="1"  
                                            as="a">
                                            <i className="fas fa-trophy cate__item"></i>Bộ sưu tập
                                        </Accordion.Toggle>
                                        <div className='dropdown-sidenav-btn'><i className='far fa-dot-circle fa-lg'></i></div>
                                        <Accordion.Collapse eventKey="1">
                                        <ul className="sub-menu">
                                           {
                                               collectIndexes.map((item: any, i: number) => (
                                                <li key={item.key} className="menu-item menu-hover">
                                                    <Link to={`/bo-suu-tap/${item.key}`} style={{color: `${color[i % 6]}`}}>{item.title}</Link>
                                                </li>
                                               ))
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
        </nav>
    )
}
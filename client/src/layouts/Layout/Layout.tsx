import React, { useCallback, useEffect, useState } from "react";
import Sidebar from "../SideBar/SideBarLeft";
import "./style.css";
import SideNavRight from "../SideBar/SideBarRight";
import { NavBar } from "../NavBar/NavBar";
import 'simplebar'; 
import 'simplebar/dist/simplebar.css';
import { Helmet } from "react-helmet";
import { ErrorBoundary } from "../../shared/ErrorBoundary/ErrorBoundary";
//Layout
function Layout({ children, title, descript }: any){

    const [isMenuLeft, setMenuLeft] = useState(false);
    const toggleMenuLeft = useCallback((): void => {
        setMenuLeft(!isMenuLeft);
    }, [setMenuLeft, isMenuLeft]);

    const [isMenuRight, setMenuRight] = useState(false);
    const toggleMenuRight = useCallback(() => {
        setMenuRight(!isMenuRight);
    }, [setMenuRight, isMenuRight]);

    useEffect(() => {
      window.scroll(0, 0);
    }, []);

    return(
        <ErrorBoundary>
            <Helmet>
                <title>{title}</title>
                <meta name="description" content={descript} />
            </Helmet>
            <div id="sidebar_menu_bg" className={isMenuLeft || isMenuRight ? "active" : ""}></div>
            <NavBar toggleMenuLeft={toggleMenuLeft} 
                    toggleMenuRight={toggleMenuRight}></NavBar>
                    
            <Sidebar isMenuLeft={isMenuLeft} setMenuLeft={setMenuLeft}></Sidebar>
            <SideNavRight isMenuRight={isMenuRight} toggleMenuRight={toggleMenuRight}></SideNavRight>
            <main className="pt-fixed">
                <div className="container-general">
                    <div className="wrapper">
                        <div id="page-content-wrapper">
                            {children}
                        </div>
                    </div>
                </div>
            </main>
        </ErrorBoundary>
    )
};

export default React.memo(Layout);
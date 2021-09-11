import Loader from "./Loader";
import "./loader.css";
import React from "react";
export default function Loading(){
    return (
        <div className="anis-transtion-loader">
            <div className="anis-loader">
                <div className="anis-loader-content">
                    <Loader></Loader>
                </div>
            </div>
        </div>
    )
}
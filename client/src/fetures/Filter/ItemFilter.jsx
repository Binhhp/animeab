import React from "react";
import Skeleton, { SkeletonTheme } from "react-loading-skeleton";

export default function ItemFilter({ ...props }){

    return (
        <div className={`filter-item ${props.dataKey}`}>
            <div className="filter-header">
                {props.title}
            </div>
            {props.items.length > 0 ?
                <div className="filter-content">
                    {props.items.map((item, i) => (
                        <div onClick={(e) => props.active(e)} data-key={item.key} key={item.key} className="f-genre-item">
                            {item.title}
                        </div>
                    ))}
                </div>
            : <SkeletonTheme color="#444446" highlightColor="#444446">
                <Skeleton style={{minHeight: `25px`}}></Skeleton>
                <Skeleton style={{minHeight: `25px`, width: `75%`}}></Skeleton>
            </SkeletonTheme>}
        </div>
    )
}
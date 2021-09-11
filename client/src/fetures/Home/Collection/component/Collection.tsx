import React from "react";
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import Skeleton, { SkeletonTheme } from "react-loading-skeleton";
import "../css/collect.css";

export default function Collection() {
    const collections = useSelector((state: any) => state.collections.data);
    const loading = [1, 2, 3, 4];

    return(
        <div className="collection">
            <div className="slideout-heading">
                <div className="d-inlinee-block">
                    <h5 className="cat-heading">Bộ sưu tập</h5>
                </div>
            </div>
            <div className="collection-list">
                <div className="collect-wrapper carousel movies">
                    {
                        (collections.length > 0 && collections.map((item: any) => (
                            <div key={item.key} className="collection-item">
                                <div className="position-relative d-block">
                                    <Link 
                                        title={item.title}
                                        to={`/bo-suu-tap/${item.key}`}>
                                        <img src={item.image}
                                            className="img-fluid shadow-sm" alt={item.title} />
                                    </Link>
                                </div>
                            </div>
                        )))
                        || loading.map(item => (
                            <div key={ `collect-${item}` } className="collection-item">
                                <SkeletonTheme color="#444446" highlightColor="#444446">
                                    <Skeleton className="skeletion-item"></Skeleton>
                                </SkeletonTheme>
                            </div>
                        ))
                    }
                </div>
            </div>
        </div>
    )
}
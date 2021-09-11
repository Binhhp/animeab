
import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { Fancybox } from '@fancyapps/ui/src/Fancybox/Fancybox';
import "@fancyapps/ui/dist/fancybox.css";
import "../css/infor.css";
import { useSelector } from "react-redux";
import formatDate from "../../../../hooks/useFormatDatetime";
import Skeleton, { SkeletonTheme } from "react-loading-skeleton";

function Information(props: any) {

    const categories = useSelector((state: any) => state.categories.data);
    var data = categories.filter((x: any) => x.key === props.anime.categoryKey);
    var category = categories.length > 0 ? (data.length > 0 ? data[0] : {}) : {};

    const [isHidden, setHidden] = useState(true);
    
    useEffect(() => {
        Fancybox.bind("[data-fancybox]", {
            showClass: "fancybox-fadeIn",
            showLoading: true,
            animated: true,
            hideScrollbar: true,
            closeButton: 'inside'
          });
    });

    return(
        <React.Fragment>
            <div className="anis-infor navigation-sticky-top"
                style={{width: `${props.sizeEpisode.width}`}}>
                <div className="text-uppercase mb-3">
                    <Link className="catlist" to={`/anime/${category?.key}`}>
                        <i className="far fa-play-circle"></i>&nbsp;&nbsp;{category.title || <Skeleton></Skeleton>}
                    </Link>
                </div>
                <div className="anis-img mb-3">
                    {(props.anime?.image && <img src={props.anime?.image} alt={props.anime?.title}/>) 
                    || <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>
                    }
                </div>
                <div className="anis-title">
                    <span>{props.anime?.title || <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>}</span>
                </div>
                <div className="anis-title-sub">
                    {props.anime?.titleVie || <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>}
                </div>
                {
                   ( props.anime?.views && 
                    <div className="anis-view mt-1">
                        <small className="view">{props.anime?.views.toLocaleString("vi-VN")} lượt xem</small>
                        <div className="dot dot-sm"></div>
                        <small className="date-release">{formatDate(props.anime.dateRelease)}</small>
                    </div>)
                    || <div className="anis-view-loading mt-1">
                        <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>
                    </div>
                }
                <div className="ml__5 mb-3">
                    <div className={`anis-descript my-3 ${isHidden ? "hidden-content" : ""}`}>
                        {props.anime.description || <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>}
                    </div>
                </div>
                <div className="ml__5 mb-3 more">
                    {
                        isHidden 
                            ?   <span onClick={() => setHidden(false)}>
                                    <small>Xem thêm&nbsp;<i className="fa fa-chevron-down"></i></small>
                                </span>
                            :   <span onClick={() => setHidden(true)}>
                                    <small>Ẩn bớt&nbsp;<i className="fa fa-chevron-up"></i></small>
                                </span>
                    }
                </div>
                <div className="anis-action py-3">
                     <div className="play-trainer mr-4">
                        <div className="d-flex">
                            <div className="anis-trainer">
                                <div className="mr-3 anis-boxes bg-danger">
                                    <i className="fas fa-film fa-2x"></i>
                                </div>
                                <div className="anis-boxes-action">
                                    <a href="javascripts:void(0);" 
                                        data-fancybox="gallery"
                                        data-src={props.anime?.trainer || ""}
                                        >Trainer</a>
                                </div>
                            </div>
                            <div className="anis-love">
                                <div className="mr-3 anis-boxes bg-danger">
                                    <i className="fas fa-heart fa-2x"></i>
                                </div>
                                <div className="anis-boxes-action">
                                    <a href="javascripts:void(0);">Yêu thích</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </React.Fragment>
    )
}

export default React.memo(Information)
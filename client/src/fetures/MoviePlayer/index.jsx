
import React, { useCallback, useEffect, useState } from "react";
import Layout from "../../layouts/Layout/Layout";
import JwPlayerVideo from "./AnimeDetail/component/VideoPlayer";
import AnimeInfor from "./AnimeDetail/component/Information";
import Episodes from "./Episodes/component/Episode";
import { useOptionSize } from "./hook/useOptionSize";
import "./style.css";
import { useParams } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { animeDetails } from "../../reduxs/doSomethings";
import Series from "./SeriesAnime/component/Series";
import Skeleton, { SkeletonTheme } from "react-loading-skeleton";
import { AnimeOffer } from "./AnimeOffer/AnimeOffer";
import AnimeRelate from "./AnimeRelate/AnimeRelate";
import Comment from "../Comment/component/Comment";
import { ActionVideo } from "./Action/ActionsVideo";

function ViewMovie(){

    const { meta, episode } = useParams();

    const dispatch = useDispatch();
    const index = useSelector(state => state.animeDetail.data);
    //List episode
    const episodeItem = useSelector(state => state.animeEpisode.data);
    //Set size
    const {size, sizeEpisode} = useOptionSize();

    const [episodeActive, setEpisodeActive] = useState(episode);
    const onVideoLoad = useCallback((event) => {
        setEpisodeActive(event.item.key);
    }, [setEpisodeActive]);

    useEffect(() => {
        setEpisodeActive(episode);
        dispatch(animeDetails(meta, episode));
    }, [dispatch, meta, episode, setEpisodeActive])

    const title = Object.keys(episodeItem).length > 0 && `${index.title} - ${episodeItem?.title.includes("Tập") 
                    ? episodeItem?.title : `Tập ${episodeItem?.episode} - ${episodeItem?.title}`}`;
    
    const description = Object.keys(episodeItem).length > 0 && `AnimeAB - ${index?.title} ${episodeItem?.title}`;

    return(
        <Layout title={title} descript={description}>      
            <div id="anis-detail" className="anis-detail col-12">
                <div className="anis">
                    <div className="anis-cover-wrap">
                        <div className="anis-cover" 
                        style={{backgroundImage: `url(${index.image})`}}> 
                        </div>
                    </div>
                    <div className="anis-video" style={{width: `${size.width}`}}>
                        <div className="episode-title-wrapper">
                            <span>
                                {(episodeItem?.title && episodeItem?.title.includes("Tập")
                                        ? episodeItem?.title 
                                        : `Tập ${episodeItem?.episode} - ${episodeItem?.title}`)
                                        
                                || <SkeletonTheme color="#444446" highlightColor="#444446">
                                        <Skeleton style={{minHeight: `14px`, height: `14px`, width: `70%`}}></Skeleton>
                                    </SkeletonTheme>}
                            </span>
                            <div className="episode-views-wrapper">
                                <small>
                                    {(episodeItem?.views && `${episodeItem?.views.toLocaleString("vi-VN")} lượt xem`)
                                    || <SkeletonTheme color="#444446" highlightColor="#444446">
                                        <Skeleton style={{minHeight: `10px`, height: `10px`, width: `50%`}}></Skeleton>
                                    </SkeletonTheme>} 
                                </small>
                            </div>
                        </div>
                        {/* -----Video/Iframe Anime----- */}
                        <div className="anis-jw-player">
                            <JwPlayerVideo 
                                titleAnime={index.title} 
                                episodeItem={episodeItem} 
                                size={size}
                                onVideoLoad={onVideoLoad}></JwPlayerVideo>
                            <ActionVideo anime={index} meta={meta} episode={episode}></ActionVideo>
                        </div>
                        {/* -----List episodes----------- */}
                        <Episodes meta={episodeActive} anime={index}></Episodes>
                        {/* ------Series Anime----------- */}
                        { index?.series && <Series animeKey={index?.key} series={index?.series}></Series>}
                        {/* ----Comments Plugin
                            anime key set comment, link notify set notify comment---- */}
                        <Comment animeKey={meta} linkNotify={`/xem-phim/${meta}/${episode}`}></Comment>
                    </div>
                    <AnimeInfor anime={index} sizeEpisode={sizeEpisode}></AnimeInfor>
                </div>
            </div>
            <div className="main-pad">
                <div className="main-anis">
                    <div className="content">
                        <AnimeOffer categoryKey={index?.categoryKey} animeKey={meta}></AnimeOffer>
                    </div>
                    <AnimeRelate categoryKey={index?.categoryKey} animeKey={meta}></AnimeRelate>
                </div>
            </div>
        </Layout>
    )
};

export default React.memo(ViewMovie);
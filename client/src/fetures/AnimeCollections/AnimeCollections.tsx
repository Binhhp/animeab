import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import Layout from "../../layouts/Layout/Layout";
import Animes from "../../shared/Animes/component/Animes";
import AreaCategories from "../../shared/AreaCategories/component/AreaCategories";
import AnimeRanks from "../../shared/Ranks/AnimeRanks";
import { animeCollections } from "../../reduxs/doSomethings";

export default function AnimeCollections(){
    const { meta } = useParams<{ meta: string }>();

    const collections: Collections[] = useSelector((state: any) => state.collections.data);
    const indexes: Collections = collections.filter((item: any) => item.key === meta)[0];

    const animeCollects = useSelector((state: any) => state.animeCollections.data);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(animeCollections(meta))
    }, [dispatch, meta]);

    return(
        <Layout 
            title={ `AnimeAB - ${indexes.title}` } 
            descript={ `AnimeAB - ${indexes.title}` }>
            <div className="main-pad">
                <div className="anis-content">
                    <div className="anis-cate">
                        <AreaCategories isIcon={true}></AreaCategories>
                    </div>
                    <div className="anis-content-wrapper">
                        <div className="content">
                            <Animes animes={animeCollects} flewBig={true} title={indexes.title}></Animes>
                        </div>
                        <AnimeRanks></AnimeRanks>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useParams } from "react-router-dom";
import Layout from "../../layouts/Layout/Layout";
import { animeCategories } from "../../reduxs/doSomethings";
import Animes from "../../shared/Animes/component/Animes";
import AreaCategories from "../../shared/AreaCategories/component/AreaCategories";
import AnimeRanks from "../../shared/Ranks/AnimeRanks";

export default function Categories(){
    const { meta } = useParams();

    const categories = useSelector(state => state.categories.data);
    const indexes = categories.filter(x => x.key === meta);

    const animeCates = useSelector(state => state.animeCategories.data);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(animeCategories(meta));
    }, [dispatch, meta]);

    return(
        <Layout 
            title={ indexes.length > 0 ? `AnimeAB - ${indexes[0].title}` : "" } 
            descript={ indexes.length > 0 ? `AnimeAB - ${indexes[0].title}` : "" }>
            
            <div className="main-pad">
                <div className="anis-content">
                    <div className="anis-cate">
                        <AreaCategories meta={meta} isIcon={true}></AreaCategories>
                    </div>
                    <div className="anis-content-wrapper">
                        <div className="content">
                            <Animes animes={animeCates} flewBig={true} title={indexes.length > 0 ? indexes[0].title : ""}></Animes>
                        </div>
                        <AnimeRanks></AnimeRanks>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
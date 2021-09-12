import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import useQuery from "../../hooks/useQuery";
import Layout from "../../layouts/Layout/Layout";
import { getAnimesFilter } from "../../reduxs/animes/apis/getAnimes";
import Animes from "../../shared/Animes/component/Animes";
import "./filter.css";
import ItemFilter from "./ItemFilter";
import React from "react";
import AnimeRanks from "../../shared/Ranks/AnimeRanks";

export default function Filter() {

    const query = useQuery();
    const keyword = (query.get("keyword") as string);
    
    const title = keyword !== null && keyword !== "" ? "Kết quả tìm kiếm theo <em>" + keyword + "</em>" : "Kết quả tìm kiếm";

    const categories = useSelector((state: any) => state.categories.data);
    const collections = useSelector((state: any) => state.collections.data);

    const animeFilters = useSelector((state: any) => state.animesFilter.data);
    const dispatch = useDispatch();

    useEffect(function(){
        dispatch(getAnimesFilter(keyword));
    }, [dispatch, keyword]);

    const activeCategories = (e: any) => {
        e.preventDefault();
        const classButton = e.target.classList;

        if(classButton.contains("active")){
            classButton.remove("active");
        }
        else{
            classButton.add("active");
        }
    };

    const activeCollect = (e: any) => {
        e.preventDefault();
        const classButton = e.target.classList;

        if(classButton.contains("active")){
            classButton.remove("active");
        }
        else{
            classButton.add("active");
        }
    };

    const filter = () => {
        const eleCate = document.querySelectorAll(".category-filter .active");
        const eleCollect = document.querySelectorAll(".collect-filter .active");

        var arrCateFilters: any = [];
        var arrCollectFilters: any = [];

        if(eleCate.length > 0){

            eleCate.forEach((el, i) => {
                const dataKey = el.getAttribute("data-key");
                arrCateFilters.push(dataKey)
            });

        }

        if(eleCollect.length > 0){

            eleCollect.forEach((el, i) => {
                const dataKey = el.getAttribute("data-key");
                arrCollectFilters.push(dataKey)
            });

        }

        dispatch(getAnimesFilter(keyword, arrCateFilters, arrCollectFilters));
    };

    return(
        <Layout title={`AnimeAB - Kết quả tìm kiếm`} descript="Tìm kiếm và xem anime free online trên AnimeAB">
            <div className="main-pad">
                <div className="anis-content">
                    <div className="anis-content-wrapper">
                        <div className="content">   
                            <div className="filter-block">
                                <ItemFilter dataKey="category-filter" title="Thể loại" items={categories} active={activeCategories}></ItemFilter>
                                <ItemFilter dataKey="collect-filter" title="Bộ sưu tập" items={collections} active={activeCollect}></ItemFilter>
                                <div className="btn-filter">
                                    <button onClick={() => filter()} type="submit">Tìm kiếm</button>
                                </div>
                            </div>
                            <div className="result-animes">
                                <Animes isLoading={true} hiddenSkeneton={true} isHtml={true} animes={animeFilters} flewBig={true} title={title}></Animes>
                                <div className="result-total">{`${animeFilters.length} kết quả`}</div>
                            </div>
                        </div>
                        <AnimeRanks></AnimeRanks>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
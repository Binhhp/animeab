import React from "react";
import Layout from "../../layouts/Layout/Layout";
import Trendings from "./Trending/component/Trending";
import Collection from "./Collection/component/Collection";
import SliderMovie from "./Slider/component/Slider";
import "./home.css";
import AreaCategories from "../../shared/AreaCategories/component/AreaCategories";
import AnimeNewContents from "./AnimeNews/AnimeNewContents";
import AnimeViewAll from "./AnimeViewAll/AnimeViewAll";
import AnimeRanks from "../../shared/Ranks/AnimeRanks";

export default function Home() {
    
    return(
        <Layout title={`AnimeAB Vietsub Online Free HD Trang chủ`} descript="Xem phim anime online free trên AnimeAB">
            <SliderMovie></SliderMovie>
            <Trendings></Trendings>
            <AreaCategories></AreaCategories>
            <div className="main-wrapper">
                <div className="main-pad">
                    <div className="main-content">
                        <div className="content">
                            <Collection></Collection>
                            <AnimeNewContents></AnimeNewContents>
                            <AnimeViewAll></AnimeViewAll>
                        </div>
                        <AnimeRanks></AnimeRanks>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
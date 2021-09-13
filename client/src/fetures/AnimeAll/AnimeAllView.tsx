import { useEffect } from "react";
import Animes from "../../shared/Animes/component/Animes";
import AnimeRanks from "../../shared/Ranks/AnimeRanks";
import Layout from "../../layouts/Layout/Layout";
import AreaCategories from "../../shared/AreaCategories/component/AreaCategories";
import { useSelector } from "react-redux";

export default function AnimeAllView() {

    const animes: Animes[] = useSelector((state: any) => state.animes.data);

    useEffect(() => {
        window.scroll(0, 0)
    }, []);

    return(
        <Layout 
            title="AnimeAB - Danh sách Anime"
            descript="Tổng hợp danh sách Anime theo từng thể loại.">
            <div className="main-pad">
                <div className="anis-content">
                    <div className="anis-cate">
                        <AreaCategories isIcon={true}></AreaCategories>
                    </div>
                    <div className="anis-content-wrapper">
                        <div className="content">
                            <Animes animes={animes} flewBig={true} title="Tất cả Anime"></Animes>
                        </div>
                        <AnimeRanks></AnimeRanks>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
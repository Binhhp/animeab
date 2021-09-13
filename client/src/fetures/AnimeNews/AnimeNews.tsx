import { useCallback, useEffect, useState } from "react";
import Animes from "../../shared/Animes/component/Animes";
import Layout from "../../layouts/Layout/Layout";
import AreaCategories from "../../shared/AreaCategories/component/AreaCategories";
import { requestGet } from "../../_axios/axiosClient";
import AnimeRanks from "../../shared/Ranks/AnimeRanks";
import { ApiController } from "../../controller/apis/controller";

export default function AnimeNews() {

    const [state, setState] = useState<Animes[]>([]);

    const getAnimeNews = useCallback(async function(){
        await setState([]);
        requestGet(ApiController.GET_ANIME_NEW(0))
            .then(async (response) => {
                const data = response.data as Animes[];
                await setState(data);
                return;
            })
            .catch(error => console.log(error.statusText))
    }, []);

    useEffect(() => {
        Promise.all([
            getAnimeNews(),
            window.scroll(0, 0)
        ])
    }, [getAnimeNews]);

    return(
        <Layout 
            title="AnimeAB - Anime mới cập nhật"
            descript="Tổng hợp anime đang công chiếu với các tập mới nhất">
            <div className="main-pad">
                <div className="anis-content">
                    <div className="anis-cate">
                        <AreaCategories isIcon={true}></AreaCategories>
                    </div>
                    <div className="anis-content-wrapper">
                        <div className="content">
                            <Animes animes={state} flewBig={true} title="Anime mới cập nhật"></Animes>
                        </div>
                        <AnimeRanks></AnimeRanks>
                    </div>
                </div>
            </div>
        </Layout>
    )
}
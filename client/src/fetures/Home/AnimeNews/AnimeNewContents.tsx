import { useEffect, useState } from "react";
import { requestGet } from "../../../_axios/axiosClient";
import Animes from "../../../shared/Animes/component/Animes";
import { ApiController } from "../../../controller/apis/controller";

export default function AnimeNewContents() {

    const [animeNews, setAnimeNews] = useState<Animes[]>([]);

    useEffect(() => {
        requestGet(ApiController.GET_ANIME_NEW(12)).then(response => {
            setAnimeNews(response.data)
        }).catch(error => console.log(error.message));
    }, [setAnimeNews])
    return(
        <Animes hiddenLoader={true} isMoreView={true} animes={animeNews} title="Anime mới cập nhật" link="/anime-moi-nhat"></Animes>
    )
}
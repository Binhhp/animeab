import { useEffect, useState } from "react";
import { requestGet } from "../../../_axios/axiosClient";
import Animes from "../../../shared/Animes/component/Animes";
import { ApiController } from "../../../controller/apis/controller";

export default function AnimeViewAll(){

    const [animeViewAll, setAnimeViewAll] = useState<Animes[]>([]);
    useEffect(() => {
        requestGet(ApiController.GET_ANIME("", 3)).then(response => {
            setAnimeViewAll(response.data)
        }).catch(error => console.log(error.message));
    }, [setAnimeViewAll])
    return(
        <Animes hiddenLoader={true} isMoreView={true} animes={animeViewAll} title="Tất cả anime" link="/animes"></Animes>
    )
}
import React, { useCallback, useEffect, useState } from "react";
import { requestGet } from "../../../_axios/axiosClient";
import AreaLeft from "../../../shared/AreaLeft/component/AreaLeft";
import { ApiController } from "../../../controller/apis/controller";

function AnimeRelate({ categoryKey, animeKey }: any){

    const [animeViewDay, setAnimeViewDay] = useState<Animes[]>([]);
    const [animeViewMonth, setAnimeVieMonth] = useState<Animes[]>([]);
    const [animeViewWeek, setAnimeViewWeek] = useState<Animes[]>([]);
    
    const getAnimeView = useCallback(function(sort) {
        requestGet(ApiController.GET_ANIME_RELATE(categoryKey, animeKey, sort))
        .then(response => {
            if(sort === "viewDay"){
                setAnimeViewDay(response.data)
            }
            if(sort === "viewWeek"){
                setAnimeViewWeek(response.data)
            }
            if(sort === "viewMonth"){
                setAnimeVieMonth(response.data)
            }
        }).catch(error => console.log(error.statusText));
    }, [setAnimeViewDay, setAnimeViewWeek, setAnimeVieMonth, categoryKey, animeKey]);
    
    const getApis = useCallback(() => Promise.all([
        getAnimeView("viewDay"),
        getAnimeView("viewWeek"),
        getAnimeView("viewMonth")
    ]), [getAnimeView]);

    useEffect(() => {
        categoryKey && getApis();
    }, [categoryKey, getApis]);

    return(
        <AreaLeft title="Anime liên quan" 
            animeDays={animeViewDay}
            animeWeeks={animeViewWeek}
            animeMonths={animeViewMonth}></AreaLeft>
    )
}

export default React.memo(AnimeRelate);
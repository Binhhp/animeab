import { useCallback, useEffect, useState } from "react";
import { controller } from "../../controller/apis/controller";
import { requestGet } from "../../_axios/axiosClient";
import Ranks from "../AreaLeft/component/AreaLeft";

export default function AnimeRanks(){
    const [animeViewDay, setAnimeViewDay] = useState([]);
    const [animeViewMonth, setAnimeVieMonth] = useState([]);
    const [animeViewWeek, setAnimeViewWeek] = useState([]);

    const getAnimeView = useCallback(function(sort){
        requestGet(controller.GET_ANIME_RANK(sort))
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
        }).catch(error => console.log(error.message));
    }, [setAnimeViewDay, setAnimeViewWeek, setAnimeVieMonth]);
    
    const getApis = useCallback(() => Promise.all([
        getAnimeView("viewDay"),
        getAnimeView("viewWeek"),
        getAnimeView("viewMonth")
    ]), [getAnimeView]);

    useEffect(() => {
       getApis();
    }, [getApis])

    return (
        <Ranks title="Bảng xếp hạng" 
            animeDays={animeViewDay}
            animeWeeks={animeViewWeek}
            animeMonths={animeViewMonth}></Ranks>
    )
}
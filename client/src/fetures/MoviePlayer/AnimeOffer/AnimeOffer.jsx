import React, { useEffect, useState } from "react";
import { requestGet } from "../../../_axios/axiosClient";
import Animes from "../../../shared/Animes/component/Animes";
import { controller } from "../../../controller/apis/controller";

export const AnimeOffer = React.memo(function AnimeOffer({ categoryKey, animeKey }){
    const [state, setState] = useState([]);
    
    useEffect(() => {
        if(categoryKey){
            requestGet(controller.GET_ANIME_OFFER(categoryKey, animeKey))
            .then(response => {
                setState(response.data);
            }).catch(error => console.log(error.statusText))
        }
    }, [categoryKey, animeKey, setState])
    return (
        <Animes hiddenLoader={true} 
                isMoreView={true} 
                link="/animes" 
                page={12} animes={state} 
                title="Animes đề xuất"></Animes>
    )
}) 
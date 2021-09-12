import { 
    getAnimeByCateOfCollect, 
    getAnimes, 
    getEpisodes, 
    updateViewAnime, 
    updateViewAnimeDetail } from "./animes/apis/getAnimes";
import { getCates } from "./categories/apis/getCates";
import { getCollects } from "./collections/apis/getCollects";

export function doSomethings(){
    return (dispatch: any) => {
        let runAway = [
            dispatch(getAnimes()),
            dispatch(getCates()),
            dispatch(getCollects()),
        ];

        Promise.all(runAway);
    };
}

export function animeDetails(animeKey: string, episode = ""){
    return (dispatch: any) => Promise.all([
        dispatch(getEpisodes(animeKey)),
        dispatch(getEpisodes(animeKey, episode)),
        dispatch(getAnimes(animeKey)),
        window.scroll(0, 0)
    ])
}

export function animeCategories(categoryKey: string){
    return (dispatch: any) => Promise.all([
        dispatch(getAnimeByCateOfCollect(categoryKey)),
        window.scroll(0, 0)
    ])
}

export function animeCollections(collectionId: string){
    return (dispatch: any) => Promise.all([
        dispatch(getAnimeByCateOfCollect("",collectionId)),
        window.scroll(0, 0)
    ])
}

export function updateView(animeKey: string, animeDetailKey: string, isViewAnime = false){
    if(isViewAnime){
        return updateViewAnimeDetail(animeKey, animeDetailKey);
    }
    
    return Promise.all([
        updateViewAnime(animeKey),
        updateViewAnimeDetail(animeKey, animeDetailKey)
    ])
}

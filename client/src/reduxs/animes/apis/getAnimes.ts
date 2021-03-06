import { Dispatch } from 'react';
import { animeService } from "../actions/anime.actions";
import { requestGet, requestPost } from "../../../_axios/axiosClient";
import { detailService } from "../actions/detail.actions";
import { ApiController } from "../../../controller/apis/controller";

export const getAnimes = (animeKey = "") => {
    return async (dispatch: any) => {
        if(animeKey !== "") {
            dispatch(detailService.detailRequest());
        }
        else{
            dispatch(animeService.animesRequest());
        }

        var url = ApiController.GET_ANIME(animeKey);

        const response = await requestGet(url);
        if (response.code > 204) {
            if(animeKey !== ""){
                dispatch(detailService.detailFailure(response.errors));
            }
            else{
                dispatch(animeService.animesFailure(response.errors));
            }
        }
        else {
            if(animeKey !== ""){
                dispatch(detailService.detailSuccess(response.data))
            }
            else{
                dispatch(animeService.animesSuccess(response.data));
            }
        }
    }
}

export const getAnimesFilter = (
    keyword: string = "", 
    cateFilters: string[] = [], 
    collectFilters: string[] = []) => {
    return async (dispatch: Dispatch<any>) => {

        dispatch(animeService.animesFilterRequest());
        var response: any = {};
        
        var path = ApiController.GET_ANIME_FILTER(keyword);

        var data = {
            CategoryFilters: cateFilters,
            CollectFilters: collectFilters
        };

        response = await requestPost(path, data);

        if (response.code > 204) {
            dispatch(animeService.animesFilterSuccess([]));
        }
        else {
            dispatch(animeService.animesFilterSuccess(response.data));
        }
    }
}

export const getEpisodes = (animeKey: string, episode = "") => {
    
    return async (dispatch: any) => {
        if(episode !== ""){
            await dispatch(detailService.episodeRequest());
        }
        else{
            await dispatch(detailService.episodeOfAnimeRequest());
        }

        var url = ApiController.GET_EPISODE(animeKey, episode);

        const response = await requestGet(url);
        
        if (response.code > 204) {
            if(episode === ""){
                dispatch(detailService.episodeOfAnimeFailure(response.errors));
            }
            else{
                dispatch(detailService.episodeFailure(response.errors));
            }
        }
        else {
            if(episode === ""){
                dispatch(detailService.episodeOfAnimeSuccess(response.data));
            }
            else{
                dispatch(detailService.episodeSuccess(response.data));
            }
        }
    }
}

export const getAnimeByCateOfCollect = (categoryKey = "", collectionId = "") => {
    return async (dispatch: any) => {
        var url = ApiController.GET_ANIMECATE_OF_ANIMECOLLECT(categoryKey, collectionId);
        
        if(categoryKey !== ""){
            await dispatch(animeService.animeCateRequest());
        }
        if(collectionId !== ""){
            await dispatch(animeService.animeCollectRequest());
        }

        const response = await requestGet(url);
        
        if (response.code > 204) {
            if(categoryKey !== "") dispatch(animeService.animeCateFailure(response.errors));
            else dispatch(animeService.animeCollectFailure(response.errors));
        }
        else {
            if(categoryKey !== "") dispatch(animeService.animeCateSuccess(response.data));
            else dispatch(animeService.animeCollectSuccess(response.data));
        }
    }
}

export const updateViewAnime = (animeKey: string) => {
    const urlView = ApiController.UPDATE_VIEW(animeKey);
    return requestGet(urlView);
}

export const updateViewAnimeDetail = (animeKey: string, animeDetailKey: string) => {
    const urlViewDetail = ApiController.UPDATE_VIEW(animeKey, animeDetailKey);
    return requestGet(urlViewDetail);
}

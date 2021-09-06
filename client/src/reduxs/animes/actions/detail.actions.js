import{ detailConstants } from "./detail.constants";

export const detailService = {
    detailRequest: () => {
        return{
            type: detailConstants.ANIMEDETAIL_REQUEST
        }
    },
    detailSuccess: (message) => {
        return{
            type: detailConstants.ANIMEDETAIL_SUCCESS,
            payload: message
        }
    },
    detailFailure: (errors) => {
        return{
            type: detailConstants.ANIMEDETAIL_FAILURE,
            payload: errors
        }
    },
    episodeOfAnimeRequest: () => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_REQUEST
        }
    },
    episodeOfAnimeSuccess: (message) => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_SUCCESS,
            payload: message
        }
    },
    episodeOfAnimeFailure: (errors) => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_FAILURE,
            payload: errors
        }
    },
    episodeRequest: () => {
        return{
            type: detailConstants.EPISODE_REQUEST
        }
    },
    episodeSuccess: (message) => {
        return{
            type: detailConstants.EPISODE_SUCCESS,
            payload: message
        }
    },
    episodeFailure: (errors) => {
        return{
            type: detailConstants.EPISODE_FAILURE,
            payload: errors
        }
    },
}

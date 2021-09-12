import{ detailConstants } from "./detail.constants";

export const detailService = {
    detailRequest: () => {
        return{
            type: detailConstants.ANIMEDETAIL_REQUEST
        }
    },
    detailSuccess: (detail: any) => {
        return{
            type: detailConstants.ANIMEDETAIL_SUCCESS,
            payload: detail
        }
    },
    detailFailure: (errors: string) => {
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
    episodeOfAnimeSuccess: (episodeOfAni: any) => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_SUCCESS,
            payload: episodeOfAni
        }
    },
    episodeOfAnimeFailure: (errors: string) => {
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
    episodeSuccess: (episode: any) => {
        return{
            type: detailConstants.EPISODE_SUCCESS,
            payload: episode
        }
    },
    episodeFailure: (errors: string) => {
        return{
            type: detailConstants.EPISODE_FAILURE,
            payload: errors
        }
    },
}

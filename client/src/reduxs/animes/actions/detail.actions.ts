import{ detailConstants } from "./detail.constants";

export const detailService = {
    detailRequest: (): IActionAnimeDetail => {
        return{
            type: detailConstants.ANIMEDETAIL_REQUEST
        }
    },
    detailSuccess: (detail: AnimeDetail): IActionAnimeDetail => {
        return{
            type: detailConstants.ANIMEDETAIL_SUCCESS,
            payload: detail
        }
    },
    detailFailure: (errors: string): IActionAnimeDetail => {
        return{
            type: detailConstants.ANIMEDETAIL_FAILURE,
            payload: errors
        }
    },
    episodeOfAnimeRequest: (): IActionAnimeDetail => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_REQUEST
        }
    },
    episodeOfAnimeSuccess: (episodeOfAni: AnimeDetail[]): IActionAnimeDetail => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_SUCCESS,
            payload: episodeOfAni
        }
    },
    episodeOfAnimeFailure: (errors: string): IActionAnimeDetail => {
        return{
            type: detailConstants.EPISODES_OF_ANIME_FAILURE,
            payload: errors
        }
    },
    episodeRequest: (): IActionAnimeDetail => {
        return{
            type: detailConstants.EPISODE_REQUEST
        }
    },
    episodeSuccess: (episode: AnimeDetail[]): IActionAnimeDetail => {
        return{
            type: detailConstants.EPISODE_SUCCESS,
            payload: episode
        }
    },
    episodeFailure: (errors: string): IActionAnimeDetail => {
        return{
            type: detailConstants.EPISODE_FAILURE,
            payload: errors
        }
    },
}

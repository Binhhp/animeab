
import{
    animeConstants
} from "./anime.constants";

export const animeService = {
    animesRequest: (): IActionAnime => {
        return{
            type: animeConstants.ANIMES_REQUEST
        }
    },
    animesSuccess: (animes: Animes[]): IActionAnime => {
        return{
            type: animeConstants.ANIMES_SUCCESS,
            payload: animes
        }
    },
    animesFailure: (errors: string): IActionAnime => {
        return{
            type: animeConstants.ANIMES_FAILURE,
            payload: errors
        }
    },
    animesFilterRequest: (): IActionAnime => {
        return{
            type: animeConstants.ANIMES_FILTER_REQUEST
        }
    },
    animesFilterSuccess: (animeFilter: Animes[]): IActionAnime => {
        return{
            type: animeConstants.ANIMES_FILTER_SUCCESS,
            payload: animeFilter
        }
    },
    animeCateRequest: (): IActionAnime => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_REQUEST
        }
    },
    animeCateSuccess: (animeCate: Animes[]): IActionAnime => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_SUCCESS,
            payload: animeCate
        }
    },
    animeCateFailure: (errors: string): IActionAnime => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_FAILURE,
            payload: errors
        }
    },
    animeCollectRequest: (): IActionAnime => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_REQUEST
        }
    },
    animeCollectSuccess: (animeCollect: Animes[]): IActionAnime => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_SUCCESS,
            payload: animeCollect
        }
    },
    animeCollectFailure: (errors: string): IActionAnime => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_FAILURE,
            payload: errors
        }
    }
}

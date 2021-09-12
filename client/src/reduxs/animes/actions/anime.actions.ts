import{
    animeConstants
} from "./anime.constants";

export const animeService = {
    animesRequest: () => {
        return{
            type: animeConstants.ANIMES_REQUEST
        }
    },
    animesSuccess: (animes: any) => {
        return{
            type: animeConstants.ANIMES_SUCCESS,
            payload: animes
        }
    },
    animesFailure: (errors: string) => {
        return{
            type: animeConstants.ANIMES_FAILURE,
            payload: errors
        }
    },
    animesFilterRequest: () => {
        return{
            type: animeConstants.ANIMES_FILTER_REQUEST
        }
    },
    animesFilterSuccess: (animeFilter: any) => {
        return{
            type: animeConstants.ANIMES_FILTER_SUCCESS,
            payload: animeFilter
        }
    },
    animeCateRequest: () => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_REQUEST
        }
    },
    animeCateSuccess: (animeCate: any) => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_SUCCESS,
            payload: animeCate
        }
    },
    animeCateFailure: (errors: string) => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_FAILURE,
            payload: errors
        }
    },
    animeCollectRequest: () => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_REQUEST
        }
    },
    animeCollectSuccess: (animeCollect: any) => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_SUCCESS,
            payload: animeCollect
        }
    },
    animeCollectFailure: (errors: string) => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_FAILURE,
            payload: errors
        }
    }
}

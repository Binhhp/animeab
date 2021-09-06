import{
    animeConstants
} from "./anime.constants.js";

export const animeService = {
    animesRequest: () => {
        return{
            type: animeConstants.ANIMES_REQUEST
        }
    },
    animesSuccess: (message) => {
        return{
            type: animeConstants.ANIMES_SUCCESS,
            payload: message
        }
    },
    animesFailure: (errors) => {
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
    animesFilterSuccess: (message) => {
        return{
            type: animeConstants.ANIMES_FILTER_SUCCESS,
            payload: message
        }
    },
    animeCateRequest: () => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_REQUEST
        }
    },
    animeCateSuccess: (message) => {
        return{
            type: animeConstants.ANIMES_CATEGORIES_SUCCESS,
            payload: message
        }
    },
    animeCateFailure: (errors) => {
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
    animeCollectSuccess: (message) => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_SUCCESS,
            payload: message
        }
    },
    animeCollectFailure: (errors) => {
        return{
            type: animeConstants.ANIMES_COLLECTIONS_FAILURE,
            payload: errors
        }
    }
}

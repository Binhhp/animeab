
import { animeConstants } from "../actions/anime.constants.js";

const initialState = {
    loading: false,
    data: [],
    error: ""
};

export const animesReducers = (state = initialState, action) =>{
    switch (action.type) {
        case animeConstants.ANIMES_REQUEST:
            return{
                ...initialState,
                loading: true
            };
        case animeConstants.ANIMES_SUCCESS:
            return{
                loading: false,
                data: action.payload,
                error: ''
            };
        case animeConstants.ANIMES_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}

export const animesFilterReducers = (state = initialState, action) =>{
    switch (action.type) {
        case animeConstants.ANIMES_FILTER_REQUEST:
            return{
                ...initialState,
                loading: true
            };
        case animeConstants.ANIMES_FILTER_SUCCESS:
            return{
                loading: false,
                data: action.payload,
                error: ''
            };
        default: return state;
    }
}


export const animesCateReducers = (state = initialState, action) =>{
    switch (action.type) {
        case animeConstants.ANIMES_CATEGORIES_REQUEST:
            return{
                ...initialState,
                loading: true
            };
        case animeConstants.ANIMES_CATEGORIES_SUCCESS:

            return {
                loading: false,
                data: action.payload,
                error: ''
            };
        case animeConstants.ANIMES_CATEGORIES_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}

export const animesCollectReducers = (state = initialState, action) =>{
    switch (action.type) {
        case animeConstants.ANIMES_COLLECTIONS_REQUEST:
            return{
                ...initialState,
                loading: true
            };
        case animeConstants.ANIMES_COLLECTIONS_SUCCESS:
            
            return {
                loading: false,
                data: action.payload,
                error: ''
            };
        case animeConstants.ANIMES_COLLECTIONS_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}

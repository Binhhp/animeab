import { Action, initialState } from './../../interface/domain';
import { detailConstants } from "../actions/detail.constants";
//GET anime detail
export const detailReducers = (state = initialState, action: Action) =>{
    switch (action.type) {
        case detailConstants.ANIMEDETAIL_REQUEST:
            return{
                ...state,
                loading: true
            };
        case detailConstants.ANIMEDETAIL_SUCCESS:
            var newArr = Object.assign({}, {
                data: action.payload
            });

            return newArr;
        case detailConstants.ANIMEDETAIL_FAILURE:
            return{
                loading: false,
                error: action.payload
            };
        default: return state;
    }
}
//GET list episodes of anime
export const episodeOfAnimeReducers = (state = initialState, action: Action) =>{
    switch (action.type) {
        case detailConstants.EPISODES_OF_ANIME_REQUEST:
            return{
                ...state,
                loading: true
            };
        case detailConstants.EPISODES_OF_ANIME_SUCCESS:
            var newArr = Object.assign({}, {
                data: action.payload
            });

            return newArr;
        case detailConstants.EPISODES_OF_ANIME_FAILURE:
            return{
                loading: false,
                error: action.payload
            };
        default: return state;
    }
}
//GET episode current anime
export const episodeReducers = (state = initialState, action: Action) =>{
    switch (action.type) {
        case detailConstants.EPISODE_REQUEST:
            return{
                ...initialState,
                loading: true
            };
        case detailConstants.EPISODE_SUCCESS:
            var newEpisodes = Object.assign({}, {
                data: action.payload
            });

            return newEpisodes;
        case detailConstants.EPISODE_FAILURE:
            return{
                loading: false,
                error: action.payload
            };
        default: return state;
    }
}
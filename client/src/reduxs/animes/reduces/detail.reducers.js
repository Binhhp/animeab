import { detailConstants } from "../actions/detail.constants";

const initialState = {
    loading: false,
    data: [],
    error: ""
};

const initialStateDetail = {
    loading: false,
    data: {},
    error: ""
};
//GET anime detail
export const detailReducers = (state = initialStateDetail, action) =>{
    switch (action.type) {
        case detailConstants.ANIMEDETAIL_REQUEST:
            return{
                ...state,
                loading: true
            };
        case detailConstants.ANIMEDETAIL_SUCCESS:
            return{
                loading: false,
                data: action.payload,
                error: ''
            };
        case detailConstants.ANIMEDETAIL_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}
//GET list episodes of anime
export const episodeOfAnimeReducers = (state = initialState, action) =>{
    switch (action.type) {
        case detailConstants.EPISODES_OF_ANIME_REQUEST:
            return{
                ...state,
                loading: true
            };
        case detailConstants.EPISODES_OF_ANIME_SUCCESS:
            return{
                loading: false,
                data: action.payload,
                error: ''
            };
        case detailConstants.EPISODES_OF_ANIME_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}
//GET episode current anime
export const episodeReducers = (state = initialStateDetail, action) =>{
    switch (action.type) {
        case detailConstants.EPISODE_REQUEST:
            return{
                ...initialStateDetail,
                loading: true
            };
        case detailConstants.EPISODE_SUCCESS:
            return{
                loading: false,
                data: action.payload,
                error: ''
            };
        case detailConstants.EPISODE_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}
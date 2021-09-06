
import { collectConstants } from "../actions/collect.constants.js";

const initialState = {
    loading: false,
    data: [],
    error: ""
};

export const collectReducers = (state = initialState, action) =>{

    switch (action.type) {
        case collectConstants.COLLECTION_REQUEST:
            return{
                ...state,
                loading: true
            };
        case collectConstants.COLLECTION_SUCCESS:
            let newArr = Object.assign({}, {
                data: action.payload
            });

            return newArr; 
        case collectConstants.COLLECTION_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}
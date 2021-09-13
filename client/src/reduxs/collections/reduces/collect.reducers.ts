import { initialState } from './../../../entities/state';
import { collectConstants } from "../actions/collect.constants";

export const collectReducers: any = (
    state = initialState, 
    action: IActionCollect)
    : StateAction => {

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
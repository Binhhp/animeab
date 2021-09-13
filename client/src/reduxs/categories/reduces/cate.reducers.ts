import { initialState } from './../../../entities/state';
import { cateConstants } from "../actions/cate.constants";

export const cateReducers = (
    state = initialState, 
    action: IActionCategory)
    : StateAction => {
    switch (action.type) {
        case cateConstants.CATEGORY_REQUEST:
            return{
                ...state,
                loading: true
            };
        case cateConstants.CATEGORY_SUCCESS:
            let newArr = Object.assign({}, {
                data: action.payload
            });
            return newArr;
        case cateConstants.CATEGORY_FAILURE:
            return{
                loading: false,
                data: "",
                error: action.payload
            };
        default: return state;
    }
}
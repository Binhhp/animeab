import { initialState, Action } from './../../interface/domain';
import { cateConstants } from "../actions/cate.constants";

export const cateReducers: any = (state = initialState, action: Action) =>{
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
import { cateConstants } from "./cate.constants";

export const cateService = {
    request: () => {
        return{
            type: cateConstants.CATEGORY_REQUEST
        }
    },
    success: (data) => {
        return{
            type: cateConstants.CATEGORY_SUCCESS,
            payload: data
        }
    },
    failture: (errors) => {
        return{
            type: cateConstants.CATEGORY_FAILURE,
            payload: errors
        }
    }
};

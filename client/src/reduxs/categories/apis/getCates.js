
import { cateService } from "../actions/cate.actions";
import { requestGet } from "../../../_axios/axiosClient";
import { controller } from "../../../controller/apis/controller";

export const getCates = () =>{
    return async dispatch => {
        if(localStorage.getItem("persist:__cate")) return;
        dispatch(cateService.request());

        let url = controller.GET_CATES;
        const response = await requestGet(url);
        if (response.code > 204) {
            dispatch(cateService.failture("Error"));
        }
        else {
            dispatch(cateService.success(response.data)); 
        }
    }
}
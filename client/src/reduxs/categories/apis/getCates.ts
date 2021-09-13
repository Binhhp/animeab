
import { cateService } from "../actions/cate.actions";
import { requestGet } from "../../../_axios/axiosClient";
import { ApiController } from "../../../controller/apis/controller";
import { Dispatch } from "react";

export const getCates = () =>{
    return async (dispatch: Dispatch<any>) => {
        if(localStorage.getItem("persist:__cate")) return;
        dispatch(cateService.request());

        let url: string = ApiController.GET_CATES();
        const response = await requestGet(url);
        if (response.code > 204) {
            dispatch(cateService.failture("Error"));
        }
        else {
            dispatch(cateService.success(response.data)); 
        }
    }
}
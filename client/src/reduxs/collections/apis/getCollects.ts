import { Dispatch } from 'react';

import { collectService } from "../actions/collect.actions";
import { requestGet } from "../../../_axios/axiosClient";
import { ApiController } from "../../../controller/apis/controller";

export const getCollects = () =>{
    return async (dispatch: Dispatch<any>) => {
        if(localStorage.getItem("persist:__col")) return;
        await dispatch(collectService.request());

        let url = ApiController.GET_COLLECTES();
        const response = await requestGet(url);
        
        if (response.code > 204) {
            await dispatch(collectService.failture("Error"));
        }
        else {
            await dispatch(collectService.success(response.data));
        }
    }
}
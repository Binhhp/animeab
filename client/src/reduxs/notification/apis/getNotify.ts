import { Dispatch } from 'react';

import { notifyActions } from "../actions/notify.actions";
import { requestGet } from "../../../_axios/axiosClient";
import { controller } from "../../../controller/apis/controller";

export const notifyService = {
    notifies: getNotifies,
    notifyLength: getNotifyCount
}
//Get all notifies
function getNotifies(user: any, notify = "") {
    return async (dispatch: Dispatch<any>) => {

        await dispatch(notifyActions.request());

        let url = controller.NOTIFY(user, notify);

        const response = await requestGet(url);
        
        if (response.code > 204) {
            await dispatch(notifyActions.success([]));
        }
        else {
            await dispatch(notifyActions.success(response.data));
        }
    }
}
///Get count notifies not read
function getNotifyCount(user: any) {
    return async (dispatch: Dispatch<any>) => {

        let apiURL = controller.NOTIFY(user, "", true);
        const response = await requestGet(apiURL);
        
        if (response.code > 204) {
            await dispatch(notifyActions.count_success(0));
        }
        else {
            await dispatch(notifyActions.count_success(response.data));
        }
    }
}
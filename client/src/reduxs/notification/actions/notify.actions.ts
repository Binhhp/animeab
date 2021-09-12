import {notifyConstants } from "./notify.constants";

export const notifyActions = {
    request: () => {
        return{
            type: notifyConstants.NOTIFY_REQUEST
        }
    },
    success: (data: any) => {
        return{
            type: notifyConstants.NOTIFY_SUCCESS,
            payload: data
        }
    },
    update: (notify: any) => {
        return{
            type: notifyConstants.NOTIFY_UPDATE,
            payload: notify
        }
    },

    count_success: (count: number) => {
        return{
            type: notifyConstants.NOTIFY_COUNT_SUCCESS,
            payload: count
        }
    },
    
    count_update: () => {
        return{
            type: notifyConstants.NOTIFY_COUNT_UPDATE
        }
    }
};

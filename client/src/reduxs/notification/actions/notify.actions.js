import {notifyConstants } from "./notify.constants";

export const notifyActions = {
    request: () => {
        return{
            type: notifyConstants.NOTIFY_REQUEST
        }
    },
    success: (data) => {
        return{
            type: notifyConstants.NOTIFY_SUCCESS,
            payload: data
        }
    },
    update: (notify) => {
        return{
            type: notifyConstants.NOTIFY_UPDATE,
            payload: notify
        }
    },

    count_success: (count) => {
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

import {collectConstants } from "./collect.constants";

export const collectService = {
    request: () => {
        return{
            type: collectConstants.COLLECTION_REQUEST
        }
    },
    success: (data: any) => {
        return{
            type: collectConstants.COLLECTION_SUCCESS,
            payload: data
        }
    },
    failture: (errors: string) => {
        return{
            type: collectConstants.COLLECTION_FAILURE,
            payload: errors
        }
    }
};

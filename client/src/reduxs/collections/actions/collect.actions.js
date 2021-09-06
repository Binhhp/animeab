import {collectConstants } from "./collect.constants.js";

export const collectService = {
    request: () => {
        return{
            type: collectConstants.COLLECTION_REQUEST
        }
    },
    success: (data) => {
        return{
            type: collectConstants.COLLECTION_SUCCESS,
            payload: data
        }
    },
    failture: (errors) => {
        return{
            type: collectConstants.COLLECTION_FAILURE,
            payload: errors
        }
    }
};

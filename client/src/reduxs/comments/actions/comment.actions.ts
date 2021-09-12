import { constants } from "./comment.constants";

export const commentAction = {
    request: () => {
        return{
            type: constants.REQUEST
        }
    },
    success: (comment: any) => {
        return{
            type: constants.SUCCESS,
            payload: comment
        }
    },
    update: (item: any) => {
        return{
            type: constants.UPDATE,
            payload: item
        }
    },
    send_request: () => {
        return{
            type: constants.SEND_REQUEST
        }
    },
    send_success: () => {
        return{
            type: constants.SEND_SUCCESS
        }
    },
    like: (idComment: string) =>  {
        return {
            type: constants.LIKE,
            payload: idComment
        }
    }
};

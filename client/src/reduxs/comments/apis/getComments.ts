
import { Dispatch } from 'react';
import { commentAction } from "../actions/comment.actions";
import { requestAuthPost, requestGet } from "../../../_axios/axiosClient";
import { toast } from "react-toastify";
import { ApiController } from "../../../controller/apis/controller";

function getAll(animeKey: string, sort = 'lastest') {
    return async (dispatch: Dispatch<any>) => {
        
        await dispatch(commentAction.request());

        let apiURL = ApiController.GET_COMMENTS(animeKey, sort);
        const response = await requestGet(apiURL);
        
        if (response.code > 204) {
            await dispatch(commentAction.success([]));
        }
        else {
            await dispatch(commentAction.success(response.data));
        }
    }
}

function update(item: any) {
    return (dispatch: Dispatch<any>) => {
        dispatch(commentAction.update(item));
    }
}

function like_comment(idComment: string) {
    return (dispatch: Dispatch<any>) => {
        dispatch(commentAction.like(idComment));
    }
}

function calculateTime(time: string | number | Date) {
    let timer: number = (new Date(time)).getTime();
    let d: number = Date.now();
    
    let cal: number = d - timer;

    let second = cal / 1000;
    if(second < 0) return `Vừa xong`;
    if(second < 60) return `Khoảng ${Math.floor(second)} giây trước`;

    let minute = cal / (1000 * 60);
    if(minute < 60) return `Khoảng ${Math.floor(minute)} phút trước`;
    
    let hours = cal / (1000 * 60 * 60);
    if(hours < 24) return `Khoảng ${Math.floor(hours)} giờ trước`;

    let day = cal / (1000 * 60 * 60 * 24);
    if(day < 7) return `Khoảng ${Math.floor(day)} ngày trước`;

    let week = cal / (1000 * 60 * 60 * 24 * 7);
    if(week < 4) return `Khoảng ${Math.floor(week)} tuần trước`;

    let month = cal / (1000 * 60 * 60 * 24 * 7 * 4);
    if(month < 12) return `Khoảng ${Math.floor(month)} tháng trước`;

    let years = cal / (1000 * 60 * 60 * 24 * 7 * 4 * 12);
    return `Khoảng ${Math.floor(years)} năm trước`;
}

export interface ISendMessage {
    event: any,
    user: any,
    userLogginedId: string,
    animeKey: string,
    comment?: any | string | "",
    userRevice?: string | "",
    linkNotify?: string | ""
}

const sendMessage = (message: ISendMessage) => {
    return async (dispatch: Dispatch<any>) => {
        if(message.event.keyCode === 13 && message.event.shiftKey === false){
            message.event.preventDefault();
            if(message.event.target.value === "") return toast.warn("Bạn hãy nhập nội dung bình luận nhé!!");
            
            let msg = message.event.target.value;
            
            await dispatch(commentAction.send_request());

            var checkUserRevice = msg.substring(msg.indexOf("@"), msg.indexOf(":"));
            
            if(checkUserRevice.length < 3 && checkUserRevice.length > 0 && message?.comment){
                message.userRevice = message.comment.userLocal
            };
    
            const person = {
                user_local: message.userLogginedId,
                name: message.user?.name,
                photo_url: message.user?.photo_url || "https://i.imgur.com/q4Gd1Wi.jpg",
                message: msg,
                reply_comment: message.comment?.key || message.comment
            };
    
            let url = ApiController.SEND_COMMENT(message.animeKey, message.linkNotify, message.userRevice);
    
            await requestAuthPost(url, JSON.stringify(person));
            await dispatch(commentAction.send_success());
            message.event.target.value = "";
        }
    }
};

async function getReplyComments(commentKey: string, animeKey: string) {
    let apiURL = ApiController.REPLY_COMMENT(animeKey, commentKey);

    const response = await requestGet(apiURL);
    return response.data;
};

export const commentService = {
    getAll,
    getReplyComments,
    update,
    calculateTime,
    sendMessage,
    like_comment
};

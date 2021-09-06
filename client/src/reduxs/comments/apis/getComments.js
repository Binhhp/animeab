

import { commentAction } from "../actions/comment.actions";
import { requestGet, requestPost } from "../../../_axios/axiosClient";
import { toast } from "react-toastify";
import { controller } from "../../../controller/apis/controller";

function getAll(animeKey, sort = 'lastest') {
    return async dispatch => {
        
        await dispatch(commentAction.request());

        let apiURL = controller.GET_COMMENTS(animeKey, sort);
        const response = await requestGet(apiURL);
        
        if (response.code > 204) {
            await dispatch(commentAction.success([]));
        }
        else {
            await dispatch(commentAction.success(response.data));
        }
    }
}

function update(item) {
    return dispatch => {
        dispatch(commentAction.update(item));
    }
}

function calculateTime(time){
    let timer = new Date(time);
    let d = Date.now();
    
    let cal = d - timer;

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

const sendMessage = (e, user, userLogginedId, animeKey, comment = "", userRevice = "",linkNotify = "") => {
    return async dispatch => {
        if(e.keyCode === 13 && e.shiftKey === false){
            e.preventDefault();
            if(e.target.value === "") return toast.warn("Bạn hãy nhập nội dung bình luận nhé!!");
            
            let msg = e.target.value;
            
            await dispatch(commentAction.send_request());

            var checkUserRevice = msg.substring(msg.indexOf("@"), msg.indexOf(":"));
            if(checkUserRevice.length < 3){
                userRevice = comment.userLocal
            };
    
            const person = {
                user_local: userLogginedId,
                name: user?.name,
                photo_url: user?.photo_url || "https://i.imgur.com/q4Gd1Wi.jpg",
                message: msg,
                reply_comment: comment.key || comment
            };
    
            let url = controller.SEND_COMMENT(animeKey, linkNotify, userRevice);
    
            await requestPost(url, JSON.stringify(person));
            await dispatch(commentAction.send_success());
            e.target.value = "";
        }
    }
};

async function getReplyComments(commentKey, animeKey) {
    let apiURL = controller.REPLY_COMMENT(animeKey, commentKey);

    const response = await requestGet(apiURL);
    return response.data;
};

export const commentService = {
    getAll,
    getReplyComments,
    update,
    calculateTime,
    sendMessage
};

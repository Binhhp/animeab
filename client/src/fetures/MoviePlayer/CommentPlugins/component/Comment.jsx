
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { hubConnection } from "../../hook/signaIrHub";
import "../css/comment.css";
import { commentService } from "../../../../reduxs/comments/apis/getComments";
import React from "react";
import SendComment from "./EventHanlder/SendComment";
import ListComment from "./ArrayList/ListComment";
import LoadingElipsis from "../../../../shared/Loading/LoadingElipsis/Loading";

function Comment({ animeKey, linkNotify }){

    const comments = useSelector(state => state.comments);
    const dispatch = useDispatch();

    const showCmt = 5;
    const [isMore, setMore] = useState(showCmt);

    useEffect(() => {
        dispatch(commentService.getAll(animeKey))
        hubConnection.on(animeKey, async (response) => {
            if(response){
                await dispatch(commentService.update(response));
            }
        })
    }, [animeKey, dispatch])

    const [valueSelect, setValueSelect] = useState("lastest");
    const onChangeSelect = (e) => {
        setValueSelect(e.target.value);
        dispatch(commentService.getAll(animeKey, e.target.value));
    };

    return(
        <div className="anime-comments">
           <div className="main-pad">
                <div className="comment-users">
                    <div className="panel-action">
                        <span>Bình luận</span>
                    </div>
                    <div className="block-comment">
                        <div className="top-block">
                            <div className="comment-count">
                                <span>{comments?.data && comments.data.length} bình luận</span>
                            </div>
                            <div className="sort-comment">
                                <span className="left">Sắp xếp theo: </span>
                                <select value={valueSelect} onChange={(e) => onChangeSelect(e)} className="form-select" id="sort-comment">
                                    <option value="lastest">Mới nhất</option>
                                    <option value="oldest">Cũ nhất</option>
                                </select>
                            </div>
                        </div>
                        <SendComment animeKey={animeKey}></SendComment>

                        {(comments?.loading && <LoadingElipsis></LoadingElipsis>)
                        || <ListComment linkNotify={linkNotify} isMore={isMore} comments={comments} animeKey={animeKey}></ListComment>}

                        {comments?.data && comments.data.filter(x => x.replyComment === "").length > showCmt && 
                            ((isMore < comments.data.filter(x => x.replyComment === "").length
                            && <span onClick={() => setMore(isMore + showCmt)} className="comment-more">Xem các bình luận trước</span>)
                            || <span onClick={() => setMore(isMore - showCmt)} className="comment-more">Ẩn bớt bình luận</span>)
                        }
                    </div>
                </div>
           </div>
        </div>
    )
}
export default React.memo(Comment);
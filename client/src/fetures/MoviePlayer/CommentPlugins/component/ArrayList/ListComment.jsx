import { useDispatch, useSelector } from "react-redux";
import { commentService } from "../../../../../reduxs/comments/apis/getComments";
import UserAction from "../DisplayComment/UserAction";
import UserAvatar from "../DisplayComment/UserAvatar";
import UserComment from "../DisplayComment/UserComment";
import ListReplyComment from "../ArrayList/ListReplyComment";
import { useState } from "react";

export default function ListComment({ comments, isMore, animeKey, linkNotify }) {
    const list = comments?.data;
    const user = useSelector(state => state.userCurrent);
    const userLogined = useSelector(state => state.userLoggedIn);

    const showMoreReply = (key, e) => {
        let elm = document.getElementById(`${window.btoa(key)}`);
        if(elm) {
            elm.querySelector('.comment-child').style.display = 'block';
            e.target.style.display = 'none';
            let sendMsgBtn = elm.querySelector('.send-comment');
            if(sendMsgBtn) sendMsgBtn.style.display = 'block';
            
        }
    };

    const [userRevice, setUserRevice] = useState("");

    const dispatch = useDispatch();

    return (
        <div className="list-comment">
            {list && 
                list.filter(x => x.replyComment === "")
                        .slice(0, isMore).map((item, i) => (
                <div className="comment-item" key={`comment-${i}`}>
                    <UserAvatar photoUrl={item.photoUrl}></UserAvatar>
                    <div className="media-right">
                        <div className="comment-content">
                            {/* user comment */}
                            <UserComment 
                                displayName={item.displayName}
                                when={item.when}></UserComment>

                            <p className="message mb-cmt">{item.message}</p>
                            {/* action */}
                            <UserAction 
                                setUserRevice={setUserRevice}
                                comment={item} 
                                count={list.filter(x => x.replyComment === item.key).length}
                                displayName={item.displayName} />

                            {list.filter(x => x.replyComment === item.key).length > 0
                                && <span 
                                    id={`show${window.btoa(item.key)}`}
                                    onClick={(e) => showMoreReply(item.key, e)} 
                                    className="more-reply">
                                        Xem thêm câu trả lời khác&nbsp;
                                        <i className="fas fa-chevron-down"></i>
                                    </span>}
                        </div>
                        <div className="list-child" id={`${window.btoa(item.key)}`}>
                            <ListReplyComment 
                                setUserRevice={setUserRevice}
                                comment={item} 
                                list={list.filter(x => x.replyComment === item.key)}></ListReplyComment>
                            {userLogined?.loggedIn &&
                            <div className="send-comment" style={{display: `none`}}>
                                <div className="box-comment">
                                    <textarea 
                                        onKeyDown={(e) => 
                                        dispatch(commentService.sendMessage(e, user, 
                                            userLogined?.user.localId, animeKey, item, userRevice, linkNotify))} 

                                        rows="2" placeholder="Viết bình luận..." className="form-control"></textarea>
                                    <span><i className="fas fa-paper-plane"></i></span>
                                </div>
                            </div>}
                        </div>
                    </div>
                </div>)
            )}
        </div>
    )
}
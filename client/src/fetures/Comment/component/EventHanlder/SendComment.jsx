import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { commentService } from "../../../../reduxs/comments/apis/getComments";
import Loading from "../../../../shared/Loading/LoadingElipsis/Loading";

function SendComment({animeKey}) {
    
    const user = useSelector(state => state.userCurrent);
    const userLogined = useSelector(state => state.userLoggedIn);

    const sendMessage = useSelector(state => state.sendMessage);
    const dispatch = useDispatch();

    return (
        <div className="comment">
            {(userLogined?.loggedIn 
                && <div className="send-comment">
                    <div className="left user-img">
                        <img src={`${user?.photo_url === "" 
                            ? "https://i.imgur.com/q4Gd1Wi.jpg" : user?.photo_url }`} alt={user?.name} />
                    </div>
                    <div className="box-comment">
                        <textarea onKeyDown={(e) => dispatch(commentService.sendMessage(e, user, userLogined?.user.localId, animeKey))} rows="2" placeholder="Viết bình luận..." className="form-control"></textarea>
                        <span><i className="fas fa-paper-plane"></i></span>
                    </div>
                    {sendMessage?.loading && <Loading></Loading>}
                </div>)
            || <span>Vui lòng đăng nhập tài khoản AnimeAB để sử dụng Bình luận</span>}
            
        </div>
    )
}
export default React.memo(SendComment);
import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { commentService, ISendMessage } from "../../../../reduxs/comments/apis/getComments";
import Loading from "../../../../shared/Loading/LoadingElipsis/Loading";

interface PropsSendComment {
    animeKey: string
}
function SendComment({ animeKey }: PropsSendComment) {
    
    const user = useSelector((state: any) => state.userCurrent);
    const userLogined = useSelector((state: any) => state.userLoggedIn);

    const sendMessage = useSelector((state: any) => state.sendMessage);
    const dispatch = useDispatch();
    const hanlderMessage = (event: React.KeyboardEvent<HTMLTextAreaElement>) => {
        const request: ISendMessage = {
            event: event,
            user: user,
            userLogginedId: userLogined?.user.localId,
            animeKey: animeKey,
        };
        dispatch(commentService.sendMessage(request))
    }
    return (
        <div className="comment">
            {(userLogined?.loggedIn 
                && <div className="send-comment">
                    <div className="left user-img">
                        <img src={`${user?.photo_url === "" 
                            ? "https://i.imgur.com/q4Gd1Wi.jpg" : user?.photo_url }`} alt={user?.name} />
                    </div>
                    <div className="box-comment">
                        <textarea 
                            onKeyDown={(e) => hanlderMessage(e)} 
                            rows={2} placeholder="Viết bình luận..." 
                            className="form-control"></textarea>
                            
                        <span><i className="fas fa-paper-plane"></i></span>
                    </div>
                    {sendMessage?.loading && <Loading></Loading>}
                </div>)
            || <span>Vui lòng đăng nhập tài khoản AnimeAB để sử dụng Bình luận</span>}
            
        </div>
    )
}
export default React.memo(SendComment);
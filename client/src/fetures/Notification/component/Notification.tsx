import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux"
import { Link } from "react-router-dom";
import { notifyActions } from "../../../reduxs/notification/actions/notify.actions";
import { notifyService } from "../../../reduxs/notification/apis/getNotify";
import { hubConnection } from "../../../hooks/signaIrHub";
import "../css/noti.css";
import CountNotifies from "./CountNotifies";
import { ListNotifies } from "./ListNotifies";

export default React.memo(function Notification() {

    const userLoggedIn = useSelector((state: any) => state.userLoggedIn);
    const [isShowBox, setShowBox] = useState(false);

    const dispatch = useDispatch();
    
    useEffect(() => {
        if(userLoggedIn?.loggedIn)
        {
            hubConnection.on(userLoggedIn?.user.localId, (response) => {
                Promise.all([
                    dispatch(notifyActions.update(response)),
                    dispatch(notifyService.notifyLength(userLoggedIn?.user.localId))
                ])
            });
        }
    }, [userLoggedIn, dispatch]);

    return (
        userLoggedIn?.loggedIn ? 
        <div className="hr-notify">
            <div className="hr-icon" onClick={() => setShowBox(!isShowBox)}>
                <i className="fas fa-bell"></i>
            </div>
            {userLoggedIn?.user && <CountNotifies user={userLoggedIn.user.localId}></CountNotifies>}
            {isShowBox && 
            <div className="new-noti-list">
                <div className="noti-header">
                    <Link to="/">
                        <i className="fas fa-check mr-2"></i>
                        <span>Thông báo</span>
                    </Link>
                </div>
                <ListNotifies user={userLoggedIn?.user.localId}></ListNotifies>
            </div>}
        </div>
        : <div></div>
    )
});
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link } from "react-router-dom";
import { notifyService } from "../../../reduxs/notification/apis/getNotify";
import Loading from "../../../shared/Loading/LoadingCircle/LoadingInfinite";
import { commentService } from "../../../reduxs/comments/apis/getComments";
import React from "react";
export function ListNotifies({ user }) {
    const notify = useSelector(state => state.notification);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(notifyService.notifies(user));
    }, [dispatch, user]);

    const onNotify = async (e, linkNotify, notifyKey) => {
        if(linkNotify === "/" || linkNotify === "")
            e.preventDefault();
        await dispatch(notifyService.notifies(user, notifyKey));
        await dispatch(notifyService.notifyLength(user));
    };

    return (
        (notify?.loading && <Loading></Loading>)
        || (notify?.data.length > 0 
            ? <div className="list-new-noti" data-simplebar>
                {notify?.data.map((item, i) => (
                    <div key={`notify-${i}`} className={`noti-item ${item?.isRead === false ? ' active' : ''}`}>
                        <Link to={item?.linkNotify} onClick={(e) => onNotify(e, item?.linkNotify, item?.key)}>
                            <div className="style-scope"></div>
                            <div className="noti-item-content">
                                <div className="notify-header">{item?.title}</div>
                                <div className="notify-renderer">
                                    {item?.message}
                                </div>
                                <div className="notify-timer">{commentService.calculateTime(item?.when)}</div>
                            </div>
                        </Link>
                    </div>
                ))}
            </div>
            : <div className="noti-content">
                <div className="block">
                    <i className="fas fa-box-open" style={{fontSize: `20px`}}></i>
                </div>
                <span>Không có thông báo nào!</span>
            </div>)
    )
}
import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux"
import { notifyService } from "../../../reduxs/notification/apis/getNotify";

export default function CountNotifies({ user }: any) {
    const notifies = useSelector((state: any) => state.notifyCount);
    const dispatch = useDispatch();

    useEffect(() => {
        dispatch(notifyService.notifyLength(user))
    },[dispatch, user])

    return (
        <div className={`notify-count${notifies.count > 0 ? '' : ' hidden'}`}>
            <span>{notifies.count}</span>
        </div> 
    )
}
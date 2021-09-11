import { commentService } from "../../../../reduxs/comments/apis/getComments";

export default function UserComment({ displayName, when }: any) {
    return (
        <div className="user-cmt mb-cmt">
            <span className="user-name">{displayName}</span>
            <span className="timer">{commentService.calculateTime(when)}</span>
        </div>
    )
}
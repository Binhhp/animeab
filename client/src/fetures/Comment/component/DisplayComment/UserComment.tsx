import { commentService } from "../../../../reduxs/comments/apis/getComments";

interface PropsComment {
    displayName: string,
    when: Date
}

export default function UserComment({ displayName, when }: PropsComment) {
    return (
        <div className="user-cmt mb-cmt">
            <span className="user-name">{displayName}</span>
            <span className="timer">{commentService.calculateTime(when)}</span>
        </div>
    )
}
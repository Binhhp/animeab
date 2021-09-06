import UserAction from "../DisplayComment/UserAction";
import UserAvatar from "../DisplayComment/UserAvatar";
import UserComment from "../DisplayComment/UserComment";

export default function ListReplyComment({ list, comment, setUserRevice }) {

    return (
        <div className="comment-child" style={{display: 'none'}}>
            {(list.map((item, i) => (
                <div className="comment-item" key={`child-${i}`}>
                    <UserAvatar photoUrl={item.photoUrl}></UserAvatar>
                    <div className="media-right">
                        <div className="comment-content">
                            <UserComment 
                                displayName={item.displayName}
                                when={item.when}></UserComment>
                            <p className="message mb-cmt">{item.message}</p>
                            <UserAction 
                                isCount={false} 
                                commentChild={item}
                                comment={comment}
                                setUserRevice={setUserRevice}/>
                        </div>
                    </div>
                </div>
            )))}
        </div>
    )
}
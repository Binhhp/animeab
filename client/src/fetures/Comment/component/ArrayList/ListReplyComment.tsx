import UserAction from "../DisplayComment/UserAction";
import UserAvatar from "../DisplayComment/UserAvatar";
import UserComment from "../DisplayComment/UserComment";

interface PropsReplyComment {
    list: any,
    comment: any,
    setUserRevice: any,
    animeKey: string
}

export default function ListReplyComment({ list, comment, setUserRevice, animeKey }: PropsReplyComment) {

    return (
        <div className="comment-child" style={{display: 'none'}}>
            {(list.map((item: any, i: number) => (
                <div className="comment-item" key={`child-${i}`}>
                    <UserAvatar photoUrl={item.photoUrl}></UserAvatar>
                    <div className="media-right">
                        <div className="comment-content">
                            <UserComment 
                                displayName={item.displayName}
                                when={item.when}></UserComment>
                            <p className="message mb-cmt">{item.message}</p>
                            <UserAction 
                                animeKey={animeKey}
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
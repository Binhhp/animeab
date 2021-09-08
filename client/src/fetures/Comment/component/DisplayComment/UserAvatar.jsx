
export default function UserAvatar({photoUrl}) {
    return (
        <div className="media-left">
            <div className="avatar">
                <img src={photoUrl} alt="Avatar"/>
            </div>
        </div>
    )
}
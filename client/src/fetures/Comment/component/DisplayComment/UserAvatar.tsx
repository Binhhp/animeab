
export default function UserAvatar({photoUrl}: any) {
    return (
        <div className="media-left">
            <div className="avatar">
                <img src={photoUrl} alt="Avatar"/>
            </div>
        </div>
    )
}
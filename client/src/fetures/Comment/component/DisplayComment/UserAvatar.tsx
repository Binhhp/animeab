interface PropsUserAvt {
    photoUrl: string
}

export default function UserAvatar({ photoUrl }: PropsUserAvt) {
    return (
        <div className="media-left">
            <div className="avatar">
                <img src={photoUrl} alt="Avatar"/>
            </div>
        </div>
    )
}

export default function EnableComment() {
    const enable = () => {
        (document.querySelector(".block-enable") as HTMLElement).style.display = `none`;
        (document.querySelector(".block-comment_content") as HTMLElement).style.display = `block`;
    };

    return (
        <div className="block-enable">
            <button title="Bình luận nào!" onClick={enable} type="submit">
                <i className="fas fa-comment mr-2"></i>
                <span>Click để bình luận</span>
            </button>
        </div>
    )
}
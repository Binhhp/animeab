
export default function CommentScroll() {
    const comment = () => {
        var element = (document.querySelector(".block-comment")as HTMLElement);
        element.scrollIntoView({behavior: 'smooth', block: "center", inline: "center"});
        return;
    };
    
    return (
        <div className="font-action" onClick={() => comment()}>
            <i className="fas fa-comment-alt mr-2"></i><span>Bình luận</span>
        </div>
    )
}
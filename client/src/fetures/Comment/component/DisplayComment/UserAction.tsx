import { hubConnection } from "../../../../hooks/signaIrHub";

interface Props {
    count?: number,
    isCount?: boolean,
    comment: any,
    commentChild?: any,
    setUserRevice?: any,
    animeKey: string
}

export default function UserAction(
    { count, isCount, comment, commentChild, setUserRevice, animeKey}: Props) {
    const showMsg = () => {

        if(commentChild){
            setUserRevice(commentChild.userLocal);
        }
        else{
            setUserRevice(comment.userLocal);
        }
        
        var ele = (document.getElementById(`${window.btoa(comment.key)}`) as HTMLElement);
        var sendComment = (ele.querySelector('.send-comment') as HTMLElement);
        if(!sendComment) return;
        
        let defaultName = commentChild?.displayName || comment?.displayName;
        if(isCount === false){
            var cmtTxt = ((ele as HTMLElement).querySelector('textarea') as HTMLTextAreaElement);
            cmtTxt.value = `@${defaultName}: `;
            cmtTxt.focus();
            return;
        }

        var showBtn = document.getElementById(`show${window.btoa(comment.key)}`);
       
        if((sendComment as HTMLElement).style.display === "none"){
            (ele.querySelector('.comment-child') as HTMLElement).style.display = 'block';
            (ele.querySelector('.send-comment') as HTMLElement).style.display = 'block';
            let textArea = (ele.querySelector('textarea') as HTMLTextAreaElement);
            textArea.value = `@${defaultName}: `;
            textArea.focus();
            
            if(ele.querySelectorAll('.comment-item').length === count && showBtn){
                showBtn.style.display = 'none'
            }
        }
        else{
            let txt = (ele.querySelector('textarea') as HTMLTextAreaElement);
            txt.value = `@${defaultName}: `;
            txt.focus();
        }
    }

    const likeComment = (e: any): void => {
        if(animeKey && comment) {
            if(e.target.classList.contains("liked")) return;
            const idComment = commentChild ? commentChild.key : comment.key;
            const data = {
                id: animeKey,
                idComment: idComment
            };
            
            hubConnection.invoke("LikeComment", data);
            e.target.classList.add("liked");
        }
        return;
    }

    return (
        <ul className="action">
            <li onClick={(e) => likeComment(e)}><i className="fas fa-heart"></i>&nbsp;&nbsp;
                {commentChild ? commentChild.likes : comment.likes}
            </li>
            <li onClick={showMsg}>
                <i className="fas fa-comment-alt"></i>&nbsp;&nbsp;
                {isCount || count}
            </li>
        </ul>
    )
}
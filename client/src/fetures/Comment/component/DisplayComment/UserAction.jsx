import { controller } from "../../../../controller/apis/controller";
import { requestAuthGet } from "../../../../_axios/axiosClient";

export default function UserAction(
    { count, isCount, comment, commentChild, setUserRevice, animeKey}) {
    const showMsg = (isCount) => {

        if(commentChild){
            setUserRevice(commentChild.userLocal);
        }
        else{
            setUserRevice(comment.userLocal);
        }
        
        var ele = document.getElementById(`${window.btoa(comment.key)}`);
        var sendComment = ele.querySelector('.send-comment');
        if(!sendComment) return;
        
        let defaultName = commentChild?.displayName || comment?.displayName;
        if(isCount === false){
            var cmtTxt = ele.querySelector('textarea');
            cmtTxt.value = `@${defaultName}: `;
            cmtTxt.focus();
            return;
        }

        var showBtn = document.getElementById(`show${window.btoa(comment.key)}`);
       
        if(sendComment.style.display === "none"){
            ele.querySelector('.comment-child').style.display = 'block';
            ele.querySelector('.send-comment').style.display = 'block';
            let textArea = ele.querySelector('textarea');
            textArea.value = `@${defaultName}: `;
            textArea.focus();
            
            if(ele.querySelectorAll('.comment-item').length === count && showBtn){
                showBtn.style.display = 'none'
            }
        }
        else{
            let txt = ele.querySelector('textarea');
            txt.value = `@${defaultName}: `;
            txt.focus();
        }
    }

    const likeComment = (e) => {
        if(animeKey && comment) {
            if(e.target.classList.contains("liked")) return;
            const idComment = commentChild ? commentChild.key : comment.key;
            requestAuthGet(controller.LIKE_COMMENT(animeKey, idComment));
            e.target.classList.add("liked");
        }
        return;
    }

    return (
        <ul className="action">
            <li onClick={(e) => likeComment(e)}><i className="fas fa-heart"></i>&nbsp;&nbsp;
                {commentChild ? commentChild.likes : comment.likes}
            </li>
            <li onClick={() => showMsg(isCount)}>
                <i className="fas fa-comment-alt"></i>&nbsp;&nbsp;
                {isCount || count}
            </li>
        </ul>
    )
}
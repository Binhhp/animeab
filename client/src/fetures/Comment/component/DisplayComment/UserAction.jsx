
export default function UserAction(
    { count, isCount, comment, commentChild, setUserRevice}) {
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

    return (
        <ul className="action">
            <li><i className="fas fa-heart"></i>&nbsp;&nbsp;0</li>
            <li onClick={() => showMsg(isCount)}>
                <i className="fas fa-comment-alt"></i>&nbsp;&nbsp;
                {isCount || count}
            </li>
        </ul>
    )
}
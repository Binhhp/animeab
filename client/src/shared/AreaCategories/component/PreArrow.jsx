import "../css/button.css";

export default function PreArrow({...props}){
    const { onClick } = props;

    return(
        <button className="pre-arrow" onClick={onClick}>
            <i className="fas fa-angle-left"></i>
        </button>
    )
}
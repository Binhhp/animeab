import "../css/button.css";

export default function NextArrow(props: any): JSX.Element{
    const { onClick } = props;
    return(
        <button className="next-arrow" onClick={onClick}>
            <i className="fas fa-angle-right"></i>
        </button>
    )
}
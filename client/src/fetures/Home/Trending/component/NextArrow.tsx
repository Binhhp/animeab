
import "../css/button.css";

export default function NextArror(props: any){
    const { onClick } = props;
    return (
      <button
       className="btn-arrow-next"
        onClick={onClick}>
        <i className="fas fa-angle-right"></i>
      </button>
    );
}
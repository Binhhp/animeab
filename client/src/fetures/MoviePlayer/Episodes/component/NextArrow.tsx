
import "../css/button.css";

export default function NextArror(props: any){
    const { onClick } = props;
    return (
      <div
       className="btn-arrow-next"
        onClick={onClick}>
        <i className="fas fa-chevron-right"></i>
      </div>
    );
}
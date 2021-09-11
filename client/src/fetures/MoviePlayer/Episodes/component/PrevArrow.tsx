
import "../css/button.css";

function PrevArrow(props: any) {
    const { onClick } = props;
    return (
      <div
        className="btn-arrow-prev"
        onClick={onClick}>
        <i className="fas fa-chevron-left"></i>
      </div>
    );
  }
  export default PrevArrow;
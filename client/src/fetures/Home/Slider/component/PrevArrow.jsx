import React from "react";
import "../css/button.css";

function PrevArrow(props) {
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
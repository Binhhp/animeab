import React from "react";
import "../css/button.css";

function PrevArrow(props: any) {
    const { onClick } = props;
    return (
      <button
        className="btn-arrow-prev"
        onClick={onClick}>
        <i className="fas fa-angle-left"></i>
      </button>
    );
  }
  export default PrevArrow;
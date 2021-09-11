import React from "react";
import "../css/button.css";

function NextArrow(props: any) {
    const { onClick } = props;
    return (
      <div
       className="btn-arrow-next"
        onClick={onClick}>
        <i className="fas fa-chevron-right"></i>
      </div>
    );
  }
  export default NextArrow;
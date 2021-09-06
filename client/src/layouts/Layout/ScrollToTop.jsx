import React from "react";
import usePosition from "../../hooks/usePosition";

export default function ScrollToTop(){
    const position = usePosition();
  
    // Set the top cordinate to 0
    // make scrolling smooth
    const scrollToTop = () => {
      window.scrollTo({
        top: 0,
        behavior: "smooth"
      });
    };

    return (
        <button onClick={() => scrollToTop()} 
          id="back-to-top" 
          className={`btn-custom ${position > 300 ? 'show' : ''}`} title="Lên nóc nhà là bắt con gà">
            <i className="fas fa-chevron-up"></i>
        </button>
    );
}

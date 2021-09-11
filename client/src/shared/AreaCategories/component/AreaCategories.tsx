
import { useSelector } from "react-redux";
import { Link } from "react-router-dom";
import Slider from "react-slick";
import "../css/area-cate.css";
import NextArrow from "./NextArrow";
import PreArrow from "./PreArrow";

export default function AreaCategories(props: any) {
    const categories: any = useSelector((state: any) => state.categories.data);

    const settings: any = {
        className: "center w-100 carousel",
        infinite: true,
        lazyLoad: true,
        speed: 500,
        autoplay: false,
        autoplaySpeed: 2500,
        centerMode: true,
        pauseOnHover: true,
        slidesToShow: 8,
        swipeToSlide: true,
        nextArrow: <NextArrow />,
        prevArrow: <PreArrow />,
        responsive: [
            {
              breakpoint: 1025,
              settings: {
                slidesToShow: 5
              }
            },
            {
                breakpoint: 769,
                settings: {
                  slidesToShow: 3
                }
              },
            {
              breakpoint: 600,
              settings: {
                slidesToShow: 2
              }
            },
            {
                breakpoint: 280,
                settings: {
                  slidesToShow: 1
                }
              }
          ]
    }; 

    const randomColor = (r: number) => {
        switch(r){
            case 1:  return " color1";
            case 2:  return " color2";
            case 3:  return " color3";
            case 4:  return " color4";
            case 5:  return " color5";
            default:  return " color6";
        }
    };

    return(
        <div className="area-categoires">
            <div className={`area-wrapper${props.isIcon ? ` hidden-area` : ''}`}>
                <div className={`area-icon${props.isIcon ? ' hidden' : ''}`}></div>
                <div className="area-title">
                    <span>Thể loại</span>
                    <p>của anime</p>
                </div>
                <div className="area-wrapper-content">
                    <Slider {...settings}>
                        {
                            categories.map((item: any, i: number) => (
                                <Link className="area-item" key={item.key} 
                                    to={`/anime/${item.key}`}>
                                    <div className={`area-item-content${randomColor(i % 6)} ${props.meta === item.key ? 'active' : ''}`}>
                                        {item.title}
                                    </div>
                                </Link>
                            ))
                        }
                    </Slider>
                </div>
            </div>
        </div>
    )
}
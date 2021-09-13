import NextArrow from "../component/NextArrow";
import PrevArrow from "../component/PrevArrow";
import "../css/slider.css";
import Slider from "react-slick";
import { Link } from "react-router-dom";
import formatDate from "../../../../hooks/useFormatDatetime";
import { useEffect, useState } from "react";
import Skeleton, { SkeletonTheme } from 'react-loading-skeleton';
import { updateView } from "../../../../reduxs/doSomethings";
import { requestGet } from "../../../../_axios/axiosClient";
import { ApiController } from "../../../../controller/apis/controller";

const settings: any = {
    dot: true,
    className: "center w-100 carousel",
    infinite: true,
    lazyLoad: true,
    speed: 500,
    autoplay: true,
    autoplaySpeed: 2500,
    pauseOnHover: true,
    slidesToShow: 1,
    swipeToSlide: true,
    nextArrow: <NextArrow />,
    prevArrow: <PrevArrow />
}; 

export default function SliderMovie() {
    const [animes, setAnimes] = useState<Animes[]>([]);

    useEffect(() => {
        requestGet(ApiController.GET_ANIME("", 0, true)).then(response => {
            setAnimes(response.data)
        }).catch(error => console.log(error.message));

    }, [setAnimes]);

    return(
        <div className="deslide-wrap">
            <div id="slider">
                {
                  (animes.length > 0 && 
                  <Slider {...settings}>
                    {
                        animes.map((item: any, i: number) => (
                            <div key={`slide-${item.key}`} className="deslide-item">
                                <div className="deslide-cover">
                                    <div className="deslide-cover-img">
                                        <img className="film-poster-img lazyloaded" 
                                            data-src={item.banner} 
                                            alt={item.title}
                                            src={item.banner} />
                                    </div>
                                </div>
                                <div className="deslide-item-content">
                                    <div className="desi-sub-text">{`#${i + 1} Spotlight`}</div>
                                    <div className="desi-head-title dynamic-name" data-jname={item.title}>
                                        {item.title}
                                    </div>
                                    <div className="sc-detail">
                                        <div className="scd-item">
                                            <i className="fas fa-play-circle mr-1"></i>TV
                                        </div>
                                        <div className="scd-item">
                                            <i className="fas fa-clock mr-1"></i>
                                            {
                                                item.movieDuration > 60 
                                                    ? `${Math.floor(item.movieDuration / 60)}h${item.movieDuration % 60}m` 
                                                    : `${item.movieDuration}m`
                                            }
                                        </div>
                                        <div className="scd-item m-hide">
                                            <i className="fas fa-calendar mr-1"></i>{formatDate(item.dateRelease)}
                                        </div>
                                        <div className="scd-item mr-1">
                                            <span className="quality">HD</span>
                                        </div>   
                                        <div className="scd-item mr-1">
                                            <span className="quality bg-white">SUB</span>
                                        </div>
                                    </div>
                                    <div className="desi-description">
                                        {item.description}
                                    </div>
                                    <div className="desi-buttons">
                                        <Link 
                                            onClick={() => updateView(item.key, item.linkStart)}
                                            to={`/xem-phim/${item.key}/${item.linkStart}`}
                                            className="btn btn-primary btn-radius mr-2">
                                            <i className="fas fa-play-circle mr-2"></i>Xem phim
                                        </Link>
                                    </div>
                                </div>
                            </div>
                        ))
                        
                    }
                </Slider>)
                ||  <SkeletonTheme color="#202125" highlightColor="#202125">
                        <Skeleton />
                    </SkeletonTheme>
                }
            </div>
        </div>
    )
};

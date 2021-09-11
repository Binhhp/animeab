import "../css/featured.css";
import React, { useEffect, useState } from "react";
import Slider from "react-slick";
import { Link } from "react-router-dom";
import NextArror from "./NextArrow";
import PrevArrow from "./PrevArrow";
import Skeleton, { SkeletonTheme } from 'react-loading-skeleton';
import { updateView } from "../../../../reduxs/doSomethings";
import { requestGet } from "../../../../_axios/axiosClient";
import { controller } from "../../../../controller/apis/controller";

export default function Trending() {
    
    const [animes, setAnimes] = useState([]);

    useEffect(() => {
        requestGet(controller.GET_ANIME("", 0, false, "views", 10, 2)).then(response => {
            setAnimes(response.data)
        }).catch(error => console.log(error.message));
    }, [setAnimes]);

    const loading = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10];

    return(
        <div className="film-trending">
            <div className="main-pad">
                <div className="slideout-heading">
                    <h5 className="cat-heading">Thịnh hành</h5>
                </div>
                <div className="film-trending-content">
                    <div className="trending-list">
                        {
                            <Slider {...settings}>
                                {
                                    (animes.length > 0 && animes.map((item: any, i: number) => (
                                        <div key={`trending-${i}`} className="film-item">
                                            <div className="img__wrap">
                                                <div className="number">
                                                    <span>{ (i + 1) < 10 ? `0${(i + 1)}` : (i + 1)}</span>
                                                    <div className="film-title">{item.title}</div>
                                                </div>
                                                <Link 
                                                title={item.title}
                                                className="film-poster" 
                                                to={`/xem-phim/${item.key}/${item.linkStart}`}
                                                onClick={() => updateView(item.key, item.linkStart)}>
                                                    <img className="film-poster-img shadow-sm"
                                                    src={item.image} alt={item.title} />
                                                </Link>
                                            </div>
                                        </div>
                                    )))

                                    || loading.map(item => (
                                        <div className="film-item" key={item}>
                                            <SkeletonTheme color="#444446" highlightColor="#444446">
                                                <Skeleton className="skeletion-item"></Skeleton>
                                            </SkeletonTheme>
                                        </div>
                                    ))
                                }
                            </Slider>
                        }
                    </div>
                    <div className="action-slider"></div>
                </div>
            </div>
        </div>
    )
}

const settings: any = {
    className: "center w-100 carousel",
    infinite: true,
    lazyLoad: true,
    speed: 500,
    autoplay: false,
    autoplaySpeed: 2500,
    pauseOnHover: true,
    slidesToShow: 8,
    slidesToScroll: 8,
    swipeToSlide: true,
    nextArrow: <NextArror />,
    prevArrow: <PrevArrow />,
    responsive: [
        {
            breakpoint: 1590,
            settings: {
              slidesToShow: 6,
              slidesToScroll: 6,
            }
          },
        {
          breakpoint: 1025,
          settings: {
            slidesToShow: 4,
            slidesToScroll: 4,
          }
        },
        {
          breakpoint: 600,
          settings: {
            slidesToShow: 4,
            slidesToScroll: 4
          }
        },
        {
            breakpoint: 280,
            settings: {
              slidesToShow: 3,
              slidesToScroll: 3
            }
          }
      ]
}; 
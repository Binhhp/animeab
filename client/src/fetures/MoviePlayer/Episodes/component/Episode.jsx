
import React from "react";
import Slider from "react-slick";
import NextArror from "./NextArrow";
import PrevArrow from "./PrevArrow";
import "../css/episode.css";
import { Link } from "react-router-dom";
import { Nav, Tab } from "react-bootstrap";
import SortSlide from "../../../../themes/svg/SortSlide";
import SortList from "../../../../themes/svg/SortList";
import Skeleton, { SkeletonTheme } from "react-loading-skeleton";
import LoadingEpisodes from "./LoadingEpisodes";
import { Items } from "./Items";
import { updateView } from "../../../../reduxs/doSomethings";
import { useSelector } from "react-redux";

export default function Episode({ ...props }){

    const episodes = useSelector(state => state.animeEpisodeArr.data);

    const settings = {
        className: "center carousel",
        infinite: true,
        speed: 500,
        slidesToShow: episodes.length > 6 ? 6 : 5,
        slidesToScroll: episodes.length > 6 ? 6 : 5,
        swipeToSlide: true,
        nextArrow: <NextArror />,
        prevArrow: <PrevArrow />,
        responsive: [
            {
              breakpoint: 1590,
              settings: {
                slidesToShow: 5,
                slidesToScroll: 5
              }
            },
            {
              breakpoint: 1025,
              settings: {
                slidesToShow: 4,
                slidesToScroll: 4
              }
            },
            {
              breakpoint: 600,
              settings: {
                slidesToShow: 3,
                slidesToScroll: 3
              }
            },
            {
              breakpoint: 480,
              settings: {
                slidesToShow: 2,
                slidesToScroll: 2
              }
            }
          ]
    };
    
    return (
        <div className="episode">
           <div className="main-pad">
              <Tab.Container defaultActiveKey="slider">
                <div className="slideout-heading">  
                    <div className="episode__title">
                      <div className="title">
                        <h5 className="cat-heading">Danh sách tập</h5>
                        <div className="list-episodes">
                          {
                              (props.anime.episode && <span>{props.anime.type === "Movie" ? 'EP full' : `EPS ${props.anime.episodeMoment} - ${props.anime.episode}`}</span>)
                              || <SkeletonTheme color="#444446" highlightColor="#444446">
                                    <Skeleton style={{minHeight: `10px`, height: `10px`, width: `30px`}}></Skeleton>
                                </SkeletonTheme>
                          }
                        </div>
                      </div>
                      <div className="toggle-episodes">
                        <Nav>
                          <Nav.Item className="toggle-list">
                            <Nav.Link eventKey="slider" className="action"><SortSlide></SortSlide></Nav.Link>
                          </Nav.Item>
                          <Nav.Item className="toggle-list">
                            <Nav.Link eventKey="list" className="action"><SortList></SortList></Nav.Link>
                          </Nav.Item>
                        </Nav>
                      </div>
                    </div>	
                </div>
                <Tab.Content>
                    <Tab.Pane eventKey="slider" className="episode-grid">
                        {
                         episodes.length > 0 ? episodes.length > 4 
                            ?<Slider {...settings}>
                                {Items(props.anime, props.meta, episodes)}
                            </Slider>
                            : <div>
                                <div className={`d-flex min-episodes ${episodes.length > 1 && ' hidden'}`}>
                                  {Items(props.anime, props.meta, episodes)}
                                </div>
                                {episodes.length > 1 && 
                                <div className="slide-min">
                                  <Slider {...settings}>
                                    {Items(props.anime, props.meta, episodes)}
                                  </Slider>
                                </div>}
                            </div>
                          : <LoadingEpisodes settings={settings}></LoadingEpisodes>
                        }
                    </Tab.Pane>
                    <Tab.Pane eventKey="list" className="episode-list">
                      <div id="list" data-simplebar>
                        <div className="wrapper-list">
                          {
                            episodes.map((item, i) => (
                              <Link 
                                onClick={() => updateView(props.anime.key, item.key)}
                                to={`/xem-phim/${props.anime.key}/${item.key}`} 
                                className={`item d-lex${props.meta === item.key ? ' active' : ''}${item.episode % 2 === 0 ? ' back-gray' : ''}`} 
                                key={`episode-${i}`}>
                                  
                                <div className="item-content">
                                  <span className="episode-number">
                                    {props.anime.episode === 1 ? 'Ep full' : item.episode}
                                  </span>
                                  <span className="episode-title">
                                    {item.title.replace(`Tập ${item.episode} - `, "")}</span>
                                  <span className="episode-icon-play"><i className="fas fa-play-circle"></i></span>
                                </div>
                              </Link>
                            ))
                          }
                        </div>
                      </div>
                    </Tab.Pane>  
                </Tab.Content>
              </Tab.Container>
           </div>
        </div>
    )
}
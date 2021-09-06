import "../css/animes.css";
import React from "react";
import { Link } from "react-router-dom";
import AnimeMapping from "./AnimeMapping";
import AnimeLoading from "./AnimeLoading";

export default function Animes({ ...props }){
   
    return (
        <div className="animes">
            <div className="slideout-heading">
                {
                    props.isHtml 
                    ? <h5 className="cat-heading" dangerouslySetInnerHTML={{__html: props.title}}></h5> 
                    : <h5 className="cat-heading">{props.title}</h5>
                }
                {
                    props.isMoreView &&
                    <Link to={props.link} className="more-anime">Xem thêm&nbsp;<i className="fas fa-angle-right ml-1"></i></Link>
                }
            </div>
            <div className="anis-list">
                <div className="film_list-wrap">
                    {
                        props.animes.length > 0 
                            ? <AnimeMapping 
                                page={props.page ?? 10}
                                flewBig={props.flewBig} 
                                animes={props.animes} 
                                hiddenLoader={props.hiddenLoader}></AnimeMapping>
                            : props.hiddenSkeneton ? "" : <AnimeLoading flewBig={props.flewBig} ></AnimeLoading>
                    }
                </div>
            </div>
        </div>
    )
}
import { useState } from "react";
import InfiniteScroll from "react-infinite-scroll-component";
import Items from "./Items";
import LoadingInfinite from "../../../shared/Loading/LoadingCircle/LoadingInfinite";

export default function AnimeMapping({ ...props }){

    const [animes, setAnimes] = useState({
        data: props.animes.slice(0, props.page),
        page: 1
    });

    const fetchMoreData = () => {
        setTimeout(function() {
            var pageFrom = animes.data.length;
            var pageTo = 12 * (animes.page + 1);
            if(props.flewBig){
                pageTo -= 2;
            }

            setAnimes({
                data: animes.data.concat(props.animes.slice(pageFrom, pageTo)),
                page: (animes.page + 1)
            })
        }, 500)
    };
    return (

        props.hiddenLoader
            ? <div className="d-fl fl-wrap">
                <Items flewBig={props.flewBig} animes={props.animes}></Items>
            </div>
            : <div id="scrollable">
                <InfiniteScroll
                    className="d-fl fl-wrap"
                    dataLength={animes.data.length}
                    next={fetchMoreData}
                    hasMore={animes.data.length < props.animes.length}
                    loader={<LoadingInfinite></LoadingInfinite>}>
                    {
                        <Items flewBig={props.flewBig} animes={animes.data}></Items>
                    }
                </InfiniteScroll>
            </div>
    )
}
import React, { useEffect, useState } from "react";
import "../css/movie.css";
import ReactJWPlayer from 'react-jw-player';
import { hanlderError, playList } from "../hook/useMovieCore";
import LoadingInfinite from "../../../../shared/Loading/LoadingCircle/LoadingInfinite";

export default function VideoPlayer({ episodes, episodeItem, titleAnime, size, onVideoLoad }){

    const iframe = `<iframe mozallowfullscreen="true" webkitallowfullscreen="true" allowfullscreen="true" src="${episodeItem.link}"></iframe>`
    const [playlist, setPlaylist] = useState([]);

    useEffect(() => {
        if(episodes && Object.keys(episodeItem).length > 0 && episodes.length > 0) {
            const videos = playList(episodes, titleAnime, episodeItem);
            setPlaylist(videos);
        }
    }, [setPlaylist, episodes, episodeItem, titleAnime]);

    return(
        <div className="anis-iframe" style={{height: `${size.height}`}}>
            {Object.keys(episodeItem).length > 0 && playlist.length > 0
                 ? episodeItem.iframe 
                    ? <div className="player-movie" dangerouslySetInnerHTML={{__html: iframe}}></div>
                    : <ReactJWPlayer
                            playerId={episodeItem.key}
                            playerScript="https://content.jwplatform.com/libraries/jvJ1Gu3c.js"
                            playlist={playlist}
                            onDisplayClick={() => (document.body.style.backgroundColor = "black")}
                            onVideoLoad={onVideoLoad}
                            customProps={hanlderError}
                        />
                : <div className="player-loading">
                    <LoadingInfinite></LoadingInfinite>
                    <div className="ld-title">Đang tải video</div>
                </div>
            }
        </div>
    )
}


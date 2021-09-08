import { useHistory } from "react-router";
import React from "react";

function PrevEpisode({ episodes, meta, episode}) {
    const history = useHistory();
    const prevEpisode = () => {
        if(episodes && episodes.length > 0) {
            const episodeIndex = episodes.findIndex(x => x.key === episode);
            let prevEpisodeIndex = episodeIndex;

            if(episodeIndex > 0){
                prevEpisodeIndex = episodeIndex - 1;
                const prevEpisode = episodes[prevEpisodeIndex];
                const url = `/xem-phim/${meta}/${prevEpisode.key}`;
                history.push(url);
            }
        }
    }

    return (
        <div onClick={() => prevEpisode()} className="font-action mr-4">
            <i className="fas fa-backward mr-2"></i><span>Tập trước</span>
        </div>
    )
}

export default React.memo(PrevEpisode);
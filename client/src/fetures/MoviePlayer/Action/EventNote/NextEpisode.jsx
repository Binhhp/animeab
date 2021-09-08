import { useHistory } from "react-router";
import React from "react";

function NextEpisode({ episodes, meta, episode}) {
    const history = useHistory();
    const nextEpisode = () => {
        if(episodes && episodes.length > 0) {
            const episodeIndex = episodes.findIndex(x => x.key === episode);
            const nextEpisode = episodes[episodeIndex + 1];
            const url = `/xem-phim/${meta}/${nextEpisode.key}`;
            history.push(url);
        }
    }

    return (
        <div onClick={() => nextEpisode()} className="font-action mr-4">
            <i className="fas fa-forward mr-2"></i><span>Tập tiếp</span>
        </div>
    )
}
export default React.memo(NextEpisode);
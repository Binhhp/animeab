import { useCallback, useState } from "react";
import { useDispatch } from "react-redux";
import { getEpisodes } from "../../../../reduxs/animes/apis/getAnimes";
import React from "react";

function ServerBackup({ animeKey, episode }: any) {
    const dispatch = useDispatch();
    const [isActive, setActive] = useState("animevsub");

    const remoteServer = useCallback((key) => {
        dispatch(getEpisodes(animeKey, episode));

        window.scrollTo({
            top: 0,
            behavior: "smooth"
        });

        setActive(key);
        return;
    }, [dispatch, animeKey, episode, setActive])

    return (
        <div className="item-server-content">
            <div className="server-info">
                <span>📢 Thông báo cho chúng tôi nếu bị lỗi hoặc không hoạt động để khắc phục. Thanks all</span>
            </div>
            <div className="server-list">
                <div className="server-list-sub">
                    <i className="fas fa-closed-captioning mr-2"></i>
                    <span>SERVER:</span>
                </div>
                <div className="server-list-content">
                    <div className={`backup${isActive === 'animevsub' ? ' active' : ''}`} onClick={() => remoteServer('animevsub')}>
                        <span>AnimeVSub</span>
                    </div>
                    <div className={`backup${isActive === 'vuighe' ? ' active' : ''}`} onClick={() => remoteServer('vuighe')}>
                        <span>Vuighe</span>
                    </div>
                </div>
            </div>
        </div>
    )
}
export default React.memo(ServerBackup);
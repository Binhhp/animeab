import React, { useCallback } from "react";
import { useSelector } from "react-redux";
import "./action-video.css";
import Note from "./EventNote/Note";
import Expand from "./EventNote/Expand";
import Light from "./EventNote/Light";
import ErrorReport from "./EventNote/ErrorReport";
import NextEpisode from "./EventNote/NextEpisode";
import PrevEpisode from "./EventNote/PrevEpisode";
import CommentScroll from "./EventNote/CommentScroll";
import ServerBackup from "./EventNote/ServerBackup";

export const ActionVideo = React.memo(function ActionVideo({ episode, meta, anime }: any) {

    const episodes = useSelector((state: any) => state.animeEpisodeArr.data);
    const note = useCallback(() => {
        if(Object.keys(anime).length > 0){
            if(anime.isStatus < 3) {
               return <Note></Note>
            }

            if(anime?.series && anime?.series !== "") {
                return <Note isCompleted={true}></Note>
            }
            else{
                return <Note isHappy={true}></Note>
            }
        }
    }, [anime]);

 
    return (
        <div className="anis-jw-action">
            <div className="position-relative">
                <div className="left d-flex item">
                    <Expand></Expand>
                    <Light></Light>
                    <ErrorReport></ErrorReport>
                </div>
                <div className="right d-flex item">
                    <PrevEpisode episodes={episodes} meta={meta} episode={episode}/>
                    <NextEpisode episodes={episodes} meta={meta} episode={episode}/>
                    <CommentScroll></CommentScroll>
                </div>
            </div>
            <div className="server w-100">
                <div className="position-relative">
                    <div className="item-server-title">
                        <div className="text-center note">
                            <span>Nếu server hiện tại không hoạt động hoặc lag, vui lòng thử các server khác bên cạnh.</span>
                        </div>
                    </div>
                    <ServerBackup episode={episode} animeKey={meta}></ServerBackup>
                </div>
            </div>
            {note()}
        </div>
    )
})
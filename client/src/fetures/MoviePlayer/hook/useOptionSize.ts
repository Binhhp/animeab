import { useEffect, useState } from "react";
import useWindowSize from "../../../hooks/useWindowSize";

export function useOptionSize(){
    const [sizeEpisode, setSizeEpisode] = useState({});
    const [size, setSize] = useState({
        width: "960px",
        height: "560px"
    });

    const widthWindow = useWindowSize().width;
    const heightWindow = useWindowSize().height;

    useEffect(() => {
        const w = widthWindow - 17;

        if(widthWindow > 1590){

            //Device website
            //padding: 16px - 65px - 17(scroll bar)
            const widthMovie = w - w * 0.03 * 2 - 60 * 2;
            //set size movie
            setSize({
                width: widthMovie * 0.7 + "px",
                height: (widthMovie * 0.7) * 0.562498 + "px"
            });
            //set size list episode
            //margin-left: 20px
            setSizeEpisode({
                width: widthMovie * 0.3 + "px"
            });

            return;
        }
        else{
            //Device mobie phone and ipad
            if(widthWindow < 1025)
            {
                //Devide ipad
                setSize({
                    width: "100%",
                    height: widthWindow * 0.5625066 + "px"
                });

                setSizeEpisode({
                    width: "100%"
                });
                return;
            }

            var widthMovie = w - w * 0.01 * 2 - 60 * 2;

            setSize({
                width: widthMovie * 0.7 + "px",
                height: (widthMovie * 0.7) * 0.562498 + "px"
            });
            //set size list episode
            //margin-left: 20px
            setSizeEpisode({
                width: widthMovie * 0.3 + "px"
            });

            return;
        }
    }, [setSize, widthWindow, heightWindow, setSizeEpisode]);

    return { size, sizeEpisode };
}
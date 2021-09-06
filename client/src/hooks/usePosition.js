import { useEffect, useState } from "react";

export default function usePosition(){
    const [position, setPosition] = useState(0);

    useEffect(function(){
       async function handlerPosition(){

            const p = await window.pageYOffset;
            
            setPosition(p)
        }

        window.addEventListener("scroll", handlerPosition);

        handlerPosition();

        return () => window.removeEventListener('scroll', handlerPosition);
    }, []);

    return position;
}
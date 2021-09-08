import { Link } from "react-router-dom";
import React from "react";

function Note({ isCompleted, isHappy }) {

    const hanlderSession = () => {
        var element = document.querySelector(".series-anime");
        element.scrollIntoView({behavior: 'smooth', block: "center", inline: "center"});
        return;
    };

    return (
        <div className="note-film" 
        style={{background: `${isCompleted ? 'rgb(100,83,148)' 
                            : (isHappy ? '#8e4785': 'rgb(133,96,136)')}`}}>
            {
                (isCompleted 
                && <span>🚀 Phim đã ra mắt các session khác#&nbsp;&nbsp;
                        <span onClick={() => hanlderSession()} className="ss-link">xem thôi!</span>
                    </span>)
                    
                || ((isHappy && <span>🚀 Hãy cùng khám phá các bộ anime hay và hấp dẫn khác#&nbsp;&nbsp;
                                    <Link to="/animes" className="ss-link">xem luôn</Link>
                                </span>)
                    || (<span>🚀 Tập tiếp theo sẽ được upload vào chủ nhật hàng tuần! Hãy đón chờ!</span>))
            }
        </div>
    )
}

export default React.memo(Note);
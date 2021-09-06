import { Link } from "react-router-dom";
import { updateView } from "../../../reduxs/doSomethings";
import stamp from "../../../assets/img/stamp.png";

export default function Items({ ...props }){
    return (
        props.animes.map((item, i) => (
            <div 
                key={`episode-${i}`} 
                className={props.flewBig && i < 4 ? "flw-item flw-item-big" : "flw-item"}>
                <div className="film-poster">
                    <div className="tick ltr">
                        <div className="tick-item tick-dub">SUB</div>
                        <div className="tick-item tick-dub">HD</div>
                    </div>
                    <div className="tick rtl">
                        <div className="tick-item tick-eps">
                            {item.type !== "Movie" ? `Ep ${item.episodeMoment}/${item.episode}` : 'Ep Full'}
                        </div>
                    </div>
                    <img data-src={item.image} 
                        className="film-poster-img lazyloaded" 
                        alt={item.title}
                        src={item.image} />

                    {item.isStatus < 2 ? "" : 
                        <Link
                            onClick={() => updateView(item.key, item.isStatus < 3 ? item.linkEnd : item.linkStart)}
                            to={`/xem-phim/${item.key}/${item.isStatus < 3 ? item.linkEnd : item.linkStart}`}  
                            className="film-poster-ahref" 
                            title={item.title}>
                            <i className="fas fa-play"></i>
                        </Link>}
                </div>
                <div className="film-detail">
                    <h3 className="film-name">
                        <Link 
                            onClick={() => updateView(item.key, item.isStatus < 3 ? item.linkEnd : item.linkStart)}
                            to={`/xem-phim/${item.key}/${item.isStatus < 3 ? item.linkEnd : item.linkStart}`}  
                            title={item.title} 
                            className="dynamic-name">
                            {item.title}
                        </Link>
                    </h3>
                    {props.flewBig && i < 4 
                        ? <div className="description">
                            {item.description}
                            </div>
                        :""}

                    <div className="fd-infor">
                        <span className="fdi-item">{item.type}</span>
                        <span className="dot"></span>
                        <span className="fdi-item fdi-duration">

                            {item.movieDuration > 60 
                                    ? `${Math.floor(item.movieDuration / 60)}h ${item.movieDuration % 60}m` 
                                    : `${item.movieDuration}m`}
                        </span>
                    </div>
                </div>
                    {item.isStatus < 2 && <div className="clear-fix"><img src={stamp} alt="Stamp"/></div>}
            </div>
        ))  
    )
}
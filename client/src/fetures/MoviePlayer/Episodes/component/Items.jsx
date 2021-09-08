import { Link } from "react-router-dom";
import { updateView } from "../../../../reduxs/doSomethings";

export const Items = (anime, meta, episodes) => {
    return (
      episodes.map((item, i) => (
        <div key={`episode-${i}`} className="episode-item">
         <div className={`position-relative d-block ${meta === item.key ? "active-anis" : ""}`}>
             <Link onClick={() => updateView(anime.key, item.key)}
                 to={`/xem-phim/${anime.key}/${item.key}`} 
                 className="imageouta"
                 title={item.title}>
                 <img src={item.image}
                     className="img-fluid shadow-sm w-100"
                     alt={item.title} />
             </Link>
         </div>
         <div className="p-2 my-md-2">
             <h6>
                {
                item.title.includes("Tập") 
                 ? (anime.episode === 1 ? `[${anime.type}] ${item.title}` : item.title) 
                 : (anime.episode === 1 ? `[${anime.type}] ${item.title}` : `Tập ${item.episode} - ${item.title}`)
                }
             </h6>
             <small>{item.views.toLocaleString("vi-VN")} lượt xem</small>
         </div>
       </div>
     ))
    )
  }
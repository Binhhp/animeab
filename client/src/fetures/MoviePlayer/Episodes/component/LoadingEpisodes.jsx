import Skeleton, { SkeletonTheme } from "react-loading-skeleton";
import Slider from "react-slick";

export default function LoadingEpisodes({settings}){

    const loading = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
    return ( 
        <SkeletonTheme color="#444446" highlightColor="#444446">
            <Slider {...settings}>
            {
                loading.map((item, i) => (
                    <div key={i} className="episode-item loading-skeneton">
                        <div className="position-relative d-block">
                            <div className="imageouta">
                                <Skeleton className="img-fluid shadow-sm w-100"></Skeleton>
                            </div>
                        </div>
                        <div className="p-2 my-md-2">
                            <h6><Skeleton style={{minHeight: `19px`}}></Skeleton></h6>
                            <small><Skeleton style={{minHeight: `14px`}}></Skeleton></small>
                        </div>
                    </div>
                )) 
            }
            </Slider>           
        </SkeletonTheme>
    )
}
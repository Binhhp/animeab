import Skeleton, { SkeletonTheme } from 'react-loading-skeleton';
import '../css/skeneton.css';

export default function AnimeLoading({ ...props }){
    const loading = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    const loadingBig = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16];

    return (
        <div className="d-fl fl-wrap">
            {
                props.flewBig 
                ? loadingBig.map(item => (
                    <div key={item} className={props.flewBig && item < 5 ? "flw-item flw-item-big" : "flw-item"}>
                        <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>
                    </div>
                ))
                : loading.map(item => (
                    <div key={item} className={props.flewBig && item < 5 ? "flw-item flw-item-big" : "flw-item"}>
                        <SkeletonTheme color="#444446" highlightColor="#444446"><Skeleton></Skeleton></SkeletonTheme>
                    </div>
                ))
            }
        </div>
    )
}
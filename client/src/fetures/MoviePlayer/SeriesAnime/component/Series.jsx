import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { controller } from "../../../../controller/apis/controller";
import { requestGet } from "../../../../_axios/axiosClient";
import "../css/series.css";

function Series({ series, animeKey }){
    const [animeSeries, setAnimeSeries] = useState([]);

    useEffect(() => {
        if(series){
            requestGet(controller.SERIES(series)).then(response => {
                
                var data = response.data;
                var result = data.sort(function(a, b){
                    return a.ordinal - b.ordinal
                });
    
                setAnimeSeries(result);
            });
        }
    }, [setAnimeSeries, series]);

    return(
        <div className="main-pad">
            <div className="series-anime">
                <table className="table-series">
                    <thead>
                        <tr>
                            <td width="60%" className="table-thead table-border-left">Danh sách anime</td>
                            <td width="20%" className="table-thead table-border-left">Thứ tự</td>
                            <td width="20%" className="table-thead">Năm</td>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            animeSeries.map((item, i) => (
                                <tr className={`table-item${item.key === animeKey ? ' series-active' : ''}`} key={`series-${i}`}>
                                    <td className="series-film table-border-left">
                                        <Link to={`/xem-phim/${item.key}/${item.linkStart}`}>{item.animeTitle}</Link>
                                    </td>
                                    <td className="series-session table-border-left">{item.session}</td>
                                    <td className="series-year">{item.yearProduce}</td>
                                </tr>
                            ))
                        }
                    </tbody>
                </table>
            </div>
        </div>
    )
};

export default React.memo(Series);
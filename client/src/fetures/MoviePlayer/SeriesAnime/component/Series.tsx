import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { ApiController } from "../../../../controller/apis/controller";
import { requestGet } from "../../../../_axios/axiosClient";
import "../css/series.css";

interface ISeries {
    series: string,
    animeKey: string
}

function Series({ series, animeKey }: ISeries) {
    const [animeSeries, setAnimeSeries] = useState([]);

    useEffect(() => {
        if(series){
            requestGet(ApiController.SERIES(series)).then(response => {
                
                var data = response.data;
                var result = data.sort(function(a: any, b: any) {
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
                            animeSeries.map((item: any, i: number) => (
                                <tr className={`table-item${item.key === animeKey ? ' series-active' : ''}`} 
                                    key={item.key}>
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
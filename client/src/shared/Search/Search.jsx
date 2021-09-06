import React, { useState } from "react"
import Autocomplete from "react-autocomplete";
import { useSelector } from "react-redux";
import { fakeRequest } from "./utils";
import "./search.css";
import "../Loading/LoadingElipsis/load.css";
import { Link, useHistory } from "react-router-dom";
import formatDate from "../../hooks/useFormatDatetime";
import Highlighter from "react-highlight-words";
import { updateView } from "../../reduxs/doSomethings";

export default function Search(){
    const list = useSelector(state => state.animes.data);
    
    const [state, setState] = useState({
      value: '',
      animes: [],
      loading: false
    });
    
    const [requestTimer, setRequestTimer] = useState(null);
    const history = useHistory();

    const hanlderSubmit = (e) => {
      e.preventDefault();
      const val = state.value;
      return history.push(`/tim-kiem?keyword=${val}`);
    };

    return (
      <form onSubmit={hanlderSubmit} className="search-content">
          <Link className="filter-icon" to="/tim-kiem"><span>Lọc</span><i className="fas fa-filter"></i></Link>
          <Autocomplete
            value={state.value}
            items={state.animes}
            inputProps={{ placeholder: 'Tìm kiếm anime', className: `input-search` }}
            getItemValue={(item) => item.title}
            onSelect={(value, state) => setState({ value, animes: [state] }) }

            onChange={e => {

              e.preventDefault();
              setState({ value: e.target.value, loading: true, animes: [] });
              clearTimeout(requestTimer);

              const data = fakeRequest(e.target.value, list, (items) => {
                setState({ animes: items, loading: false, value: e.target.value })
              });

              setRequestTimer(data);
            }}
            renderItem={(item, isHighlighted) => (
              <Link 
                  onClick={() => updateView(item.key, item.isStatus < 3 ? item.linkEnd : item.linkStart)}
                  
                  to={`/xem-phim/${item.key}/${item.isStatus < 3 ? item.linkEnd : item.linkStart}`}  
                  className={`film-item ${isHighlighted ? 'active' : ''}`}
                  key={item.key}>

                  <div className="film-poster">
                      <img src={item.image} alt={item.title} />
                  </div>
                  <div className="film-detail">
                      <h3 className="film-name">
                        <Highlighter 
                          highlightClassName="keyword"
                          searchWords={[state.value]}
                          autoEscape={true}
                          textToHighlight={item.title}/>
                      </h3>
                      <div className="film-description">
                        <Highlighter 
                            highlightClassName="keyword"
                            searchWords={[state.value]}
                            autoEscape={true}
                            textToHighlight={item.titleVie}/>
                      </div>
                      <div className="film-infor">
                          <span>{formatDate(item.dateRelease)}</span>
                          <i className="dot"></i>
                          <span>23m</span>
                      </div>
                  </div>

              </Link>
            )}
            renderMenu={(items, value) => (
              <div className="search-result-pop">
                {
                  value === ""
                  
                  ? <div className="search-infor">Nhập anime cần tìm kiếm</div>
                  
                  : state.loading === true
                    ?  <div className="loading-relative" id="search-loading">
                          <div className="loading">
                              <div className="span1"></div>
                              <div className="span2"></div>
                              <div className="span3"></div>
                          </div>
                      </div>
                  
                    : items.length === 0 
                    
                      ? (<div className="result" data-simplebar>
                          <span className="no-result">Không có anime tìm kiếm cho <strong>{value}</strong></span>
                        </div>) 

                      : (<div className="result-animes">
                          {items}
                          <Link to={`/tim-kiem?keyword=${state.value}`} className="btn-view-all">
                              <span>Xem tất cả animes&nbsp;<i className="fa fa-angle-right ml-2"></i></span>
                          </Link>
                        </div>)
                }
              </div>
            )}></Autocomplete>

        <div className="search-icon"><i className="fas fa-search"></i></div>
      </form>
    )
  }

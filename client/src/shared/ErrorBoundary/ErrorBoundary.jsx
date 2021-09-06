import React from "react";
import { Link } from "react-router-dom";
import Layout from "../../layouts/Layout/Layout";
import "./error-boundary.css";

export class ErrorBoundary extends React.Component{
    constructor(props) {
        super(props);
        this.state = { hasError: false };
      }

      componentDidCatch(error, errorInfo) {
            // console.clear();
      }

      static getDerivedStateFromError(error) {
        return { hasError: true };
      }
    
      render() {
        if (this.state.hasError) {
          return  <Layout title="AnimeAB! Đã xảy ra sự cố." descript="AnimeAB VietSub Online Free">
                    <div className="mg-hanlder">
                        <div className="error-halnder-wrapper">
                            <div id="hanlderway">
                                <div className="hanlder-container">
                                    <div id="cat1" className="painting">
                                        <img src="//s.imgur.com/images/404/cat1weyes.png" alt="cat eyes"/>
                                        <div className="eye-container">
                                            <div className="eye left" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `1.05705px`, top: `3.76376px`}}></div>
                                            </div>
                                            <div className="eye right" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.946641px`, top: `3.70013px`}}></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="cat2" className="painting">
                                        <img src="//s.imgur.com/images/404/cat2weyes.png" alt="cat 2 eyes"/>
                                        <div className="eye-container">
                                            <div className="eye" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.4488px`, top: `3.26245px`}}></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="giraffe" className="painting">
                                        <img src="//s.imgur.com/images/404/giraffeweyes.png" alt="giraff eyes"/>
                                        <div className="eye-container">
                                            <div className="eye left" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.13074px`, top: `2.71124px`}}></div>
                                            </div>
                                            <div className="eye right" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.103911px`, top: `2.63628px`}}></div>
                                            </div>
                                        </div>
                                        <img className="monocle" src="//s.imgur.com/images/404/monocle.png" alt="monocle eyes"/>
                                    </div>
                                    <div id="cat3" className="painting">
                                        <img src="//s.imgur.com/images/404/cat3weyes.png" alt="cat 3 eyes"/>
                                        <div className="eye-container">
                                            <div className="eye left" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.0787868px`, top: `2.55582px`}}></div>
                                            </div>
                                            <div className="eye right" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.0745673px`, top: `2.54103px`}}></div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="cat4" className="painting">
                                        <img src="//s.imgur.com/images/404/cat4weyes.png" alt="cat 4 eyes" />
                                        <div className="eye-container">
                                            <div className="eye left" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.0508794px`, top: `2.44825px`}}></div>
                                            </div>
                                            <div className="eye right" style={{position: `relative`}}>
                                                <div className="pupil" style={{position: `absolute`, left: `0.0493526px`, top:`2.44156px`}}></div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="error-hanlder-content text-center">
                        <div className="error-hanlder-medium">Ồ men, Đã xảy ra sự cố! Hãy thử lại sau!</div>
                        <div className="error-hanlder-small">Có gì đó sai sai đang diễn ra, reload lại trang hoặc click bên dưới để về trang chủ</div>
                        <div className="error-hanlder-button">
                            <Link to="/" className="btn btn-radius btn-focus">
                                <i className="fa fa-chevron-circle-left mr-2"></i>Về trang chủ thôi
                            </Link>
                        </div>
                    </div>
                </Layout>
        }
        return this.props.children;
      }
}
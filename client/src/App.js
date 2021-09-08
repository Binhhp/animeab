import { BrowserRouter as Router, Switch } from 'react-router-dom';
import Route from './routes/route';
import React, { Suspense, useCallback, useEffect } from 'react';
import { toast, ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css';
import "./assets/css/main.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import "slick-carousel/slick/slick.css"; 
import "slick-carousel/slick/slick-theme.css";
import ScrollToTop from "./layouts/Layout/ScrollToTop";
import { useDispatch } from 'react-redux';
import { doSomethings } from './reduxs/doSomethings';
import Loading from "./shared/Loading/LoadingFilm/Loading";
import { hubConnection } from './hooks/signaIrHub';

toast.configure();
// console.log = console.warn = console.error = () => {};
export default function App() {
    const dispatch = useDispatch();
    
    const startHub = useCallback(() => {
        hubConnection.start().catch(error => console.log("Error connect SignaIR " + error));
    }, []);

    useEffect(() => {
        dispatch(doSomethings());
        startHub();
    }, [dispatch, startHub]);
    
    return (
        <React.Fragment>
            <Router>
                <Suspense fallback={<Loading />}>
                    <div className="animeAB-wrapper">
                        <Switch>
                            <Route />
                        </Switch>
                    </div>
                </Suspense>
            </Router>
            {/* scroll to top */}
            <ScrollToTop></ScrollToTop>
            {/* toast notification */}
            <ToastContainer 
                icon={true}
                theme='colored'
                autoClose={2000} 
                closeButton={true} 
                hideProgressBar={false} 
                position={'top-right'} />
        </React.Fragment>
    )
}
    
    
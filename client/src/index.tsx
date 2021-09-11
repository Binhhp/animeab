import React from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import App from './App';
import config from "./reduxs/store";
import Footer from "./layouts/Layout/Footer";
import Favicon from 'react-favicon';
import { PersistGate } from 'redux-persist/integration/react'
import Loading from './shared/Loading/LoadingFilm/Loading';

ReactDOM.render(
  <React.Fragment>
    <Favicon url="favicon.ico?v=2"></Favicon>
    <div id="animeAB">
      <Provider store={config.store}>
        <PersistGate loading={<Loading/>} persistor={config.persistor}>
          <App />
        </PersistGate>
      </Provider>
    </div>
    <Footer></Footer>
  </React.Fragment>,
  document.getElementById('root')
);

(module as any).hot.accept();

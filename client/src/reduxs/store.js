import { applyMiddleware, createStore } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import thunk from "redux-thunk";
import { persistStore } from 'redux-persist'
import persistedReducer from "./rootReducers";

const middleware = [thunk];

const store = createStore(
    persistedReducer,
    composeWithDevTools(applyMiddleware(...middleware))
);

const persistor = persistStore(store);

const config = { store, persistor };
export default config;
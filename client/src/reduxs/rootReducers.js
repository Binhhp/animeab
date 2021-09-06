import { combineReducers } from "redux";
import { cateReducers } from "./categories/reduces/cate.reducers";
import { collectReducers } from "./collections/reduces/collect.reducers";
import { 
    animesCateReducers, 
    animesCollectReducers, 
    animesFilterReducers, 
    animesReducers } from "./animes/reduces/anime.reducers";
import { 
    detailReducers, 
    episodeOfAnimeReducers,
    episodeReducers } from "./animes/reduces/detail.reducers";
import { AuthenticateReducer } from "./user/reducers/AuthenticateReducer";
import { RegistrationReducer } from "./user/reducers/RegistrationReducer";
import { UserChangePasswordReducer, UserProfileReducer, UserReducer } from "./user/reducers/UserReducer";
import { PasswordReducer } from "./user/reducers/PasswordReducer";
import storage from 'redux-persist/lib/storage';
import { persistReducer } from 'redux-persist';
import { commentReducer, sendMessageReducer } from './comments/reducers/commentReducer';
import { notifyReducers } from "./notification/reducers/notify.reducers";
import { notifyCountReducers } from "./notification/reducers/notify_count.reducers";

const collectConfig = {
    key: '__col',
    storage
}

const cateConfig = {
    key: '__cate',
    storage
}

const rootReducers = combineReducers({
    categories: persistReducer(cateConfig, cateReducers),
    collections: persistReducer(collectConfig, collectReducers),
    animes: animesReducers,
    animesFilter: animesFilterReducers,
    animeCategories: animesCateReducers,
    animeCollections: animesCollectReducers,
    animeDetail: detailReducers,
    animeEpisodeArr: episodeOfAnimeReducers,
    animeEpisode: episodeReducers,
    userLoggedIn: AuthenticateReducer,
    userRegister: RegistrationReducer,
    userCurrent: UserReducer,
    userPassword: PasswordReducer,
    comments: commentReducer,
    sendMessage: sendMessageReducer,
    notification: notifyReducers,
    notifyCount: notifyCountReducers,
    profile: UserProfileReducer,
    changePassword: UserChangePasswordReducer
});

const persistedReducer = rootReducers;


export default persistedReducer;
import React, { lazy } from "react";
import { Route, Switch } from "react-router";
const Profile = lazy(() => import("../fetures/UserProfile/UserHome"));
const AnimeAllView = lazy(() => import("../fetures/AnimeAll/AnimeAllView"));
const AnimeNews = lazy(() => import("../fetures/AnimeNews/AnimeNews"));
const Filter = lazy(() => import("../fetures/Filter/Filter"));
const NoMatch = lazy(() => import("../fetures/NoMatch/NoMatch"));
const MoviePlayer = lazy(() => import("../fetures/MoviePlayer"));
const Home = lazy(() => import("../fetures/Home"));
const Categories = lazy(() => import("../fetures/AnimeCategories/AnimeCategories"));
const AnimeCollections = lazy(() => import("../fetures/AnimeCollections/AnimeCollections"));

export default function route() {
    
    return (
        <Switch>
            <Route exact path="/"><Home /></Route>
            <Route exact path="/animes"><AnimeAllView /></Route>
            <Route exact path="/anime-moi-nhat"><AnimeNews /></Route>
            <Route exact path="/anime/:meta"><Categories /></Route>
            <Route exact path="/bo-suu-tap/:meta"><AnimeCollections /></Route>
            <Route exact path="/xem-phim/:meta/:episode"><MoviePlayer /></Route>
            <Route exact path="/tim-kiem"><Filter /></Route>
            <Route exact path="/user/:meta"><Profile /></Route>
            <Route exact path="*"><NoMatch /></Route>
        </Switch>
    )
}
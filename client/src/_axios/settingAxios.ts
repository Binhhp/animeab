import { Cookies } from './cookies';
import axios from "axios";
import config from "../reduxs/store";
import { userService } from "../reduxs/user/api/userService";
import { requestPost } from "./axiosClient";

const apiURL = process.env.REACT_APP_API_URL;
let options = {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
};

let instance = axios.create({
    baseURL: apiURL,
    headers: options
});

let instanceAuth = axios.create({
    baseURL: apiURL,
    headers: {
        'Accept': 'application/json'
    }
});

(function(){

    instanceAuth.interceptors.request.use(config => {
        if(!Cookies.checkCookies("ac_user")) return Promise.reject("Interval Server Error");

        const access_token = Cookies.getCookies("ac_user");

        if(access_token && config) {
            config.headers.Authorization = `Bearer ${access_token}`;
        }
        return config;
    }, error => {
        
        return Promise.reject(error);
    });

    instanceAuth.interceptors.response.use(response => {
        return response;
    }, async (error) => {
        let orginalRequest = error.config;
    
        if(error.response.status === 401 && !orginalRequest._retry) {

            orginalRequest._retry = true;
    
            try {
                const rs = await refreshToken();
                const { id_token, refresh_token } = rs?.data;
                Cookies.setCookies("rf_user", refresh_token, 30);
                Cookies.setCookies("ac_user", id_token, 30);

                instance.defaults.headers.Authorization = `Bearer ${id_token}`;
                orginalRequest.headers.Authorization = `Bearer ${id_token}`;
                
                return instance(orginalRequest);
            }
            catch(_error) {
                return Promise.reject(_error);
            }
        }
    
        return Promise.reject(error);
    });

    
})()

export async function refreshToken() {

    if(!Cookies.checkCookies('rf_user')) {
        await config.store.dispatch(userService.logout());
        return Promise.reject("Error No Refresh");
    }

    const refresh_token = Cookies.getCookies('rf_user');

    const data = {
        refresh_token: refresh_token
    };

    const response = await requestPost("client/refresh_token", JSON.stringify(data)).catch(async error => {
            await config.store.dispatch(userService.logout());
            return Promise.reject(error);
        });

    if(response.code > 204) {
        await config.store.dispatch(userService.logout());
        return Promise.reject("Unauthorize");
    }

    return response;
}

export {
    instance,
    instanceAuth
};
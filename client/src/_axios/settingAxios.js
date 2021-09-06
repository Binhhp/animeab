import axios from "axios";
import QueryString from "qs";
import config from "../reduxs/store";
import { userService } from "../reduxs/user/api/userService";
import { cookies } from "./cookies";

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
        if(!cookies.checkCookie("ac_user")) return Promise.reject("Interval Server Error");

        const access_token = cookies.getCookie("ac_user");

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
                cookies.setCookie("rf_user", refresh_token, 30);
                cookies.setCookie("ac_user", id_token, 30);

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

    if(!cookies.checkCookie('rf_user')) {
        await config.store.dispatch(userService.logout());
        return Promise.reject("Error No Refresh");
    }

    const refresh_token = cookies.getCookie('rf_user');

    const urlRefresh = process.env.REACT_APP_REFRESH_TOKEN;
    const data = {
        grant_type: "refresh_token",
        refresh_token: refresh_token
    };

    const options = {
        'Content-Type': 'application/x-www-form-urlencoded'
    };
    const response = await axios.post(urlRefresh, QueryString.stringify(data), {
            headers: options
        }).catch(async error => {
            await config.store.dispatch(userService.logout());
            return Promise.reject(error);
        });

    return response;
}

export {
    instance,
    instanceAuth
};
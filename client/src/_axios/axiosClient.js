
import responseStatus from "./responseStatus";
import { instance, instanceAuth } from "./settingAxios";
//Request Https Get
export async function requestGet(url) {
    
    const response = await instance.get(url).catch(error => {
        return {
            status: error.response.status,
            statusText: error.message,
            errors: error.response.data === "" ? error.response.statusText : error.response.data,
            location: url
        };
    });

    return responseStatus(response.status, response.statusText, response.data, response.errors);
};
//Request Https Post
export async function requestPost(url, data) {

    const response = await instance.post(url, data).catch(error => {

        return {
            status: error.response.status,
            statusText: error.message,
            errors: error.response.data === "" ? error.response.statusText : error.response.data,
            location: url
        };
    });

    return responseStatus(response?.status, response?.statusText, response?.data, response?.errors);
};

export async function requestAuthGet(url) {
    
    const response = await instanceAuth.get(url).catch(error => {

        return {
            status: error.response.status,
            statusText: error.message,
            errors: error.response.data === "" ? error.response.statusText : error.response.data,
            location: url
        };
    });

    return responseStatus(response.status, response.statusText, response.data, response.errors);
};
//Request Https Post
export async function requestAuthPost(url, data) {

    const response = await instanceAuth.post(url, data, {
        headers: {
            'Content-Type': 'application/json',
        }
    }).catch(error => {

        return {
            status: error.response.status,
            statusText: error.message,
            errors: error.response.data === "" ? error.response.statusText : error.response.data,
            location: url
        };
    });

    return responseStatus(response?.status, response?.statusText, response?.data, response?.errors);
};


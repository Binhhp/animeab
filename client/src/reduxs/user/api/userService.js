
import { toast } from "react-toastify";
import { requestAuthGet, requestAuthPost, requestGet, requestPost } from "../../../_axios/axiosClient";
import { cookies } from "../../../_axios/cookies";
import { useActions } from "../action/UserAction";

export const userService = {
    login,
    logout,
    register,
    password,
    updateProfile,
    changePassword
};

function login(email, password){
    return async dispatch => {
        
        await dispatch(useActions.loginRequest());
        const url = `/client?email=${email}&&password=${password}`;

        const response = await requestGet(url);
        if(response.code > 204){
            toast.error(response.errors, {
                closeButton: true,
                closeOnClick: true,
                autoClose: false
            });
            await dispatch(useActions.loginFailture({}));
        }
        else{
            const user = response.data;

            localStorage.setItem("LOGIN_INFO", JSON.stringify(user, ['localId']));
            cookies.setCookie("ac_user", user.token, 30);
            cookies.setCookie("rf_user", user.refresh_token, 30);

            await dispatch(useActions.loginSuccess(user));
            toast.success("Đăng nhập thành công", {
                position: "bottom-right"
            })
        }
    }
}

function logout() {
    return dispatch => {
        Promise.all([
            cookies.setCookie("ac_user", "", -1),
            cookies.setCookie("rf_user", "", -1),
            localStorage.removeItem("__user"),
            localStorage.removeItem("LOGIN_INFO"),

            dispatch(useActions.getUserClear()),
            dispatch(useActions.loginClear())
        ]);
    }
}

function register(user){
    return async dispatch => {
        
        await dispatch(useActions.registerRequest());
        const url = `/client`;
        const data = JSON.stringify(user);

        const response = await requestPost(url, data);
        if(response.code > 204){
            toast.error(response.errors);
            await dispatch(useActions.registerFailture());
        }
        else{
            toast.success(response.data + " 👌", {
                closeOnClick: true,
                closeButton: true,
                autoClose: false
            });
            await dispatch(useActions.registerSuccess());
        }
    }
}

export function getUser(){
    return async dispatch => {
        if(localStorage.getItem("__user")){
            return;
        }

        let user = JSON.parse(localStorage.getItem("LOGIN_INFO"));
        if(user === null) return;

        await dispatch(useActions.getUserRequest());

        const url = `/client/${user.localId}`;
        const response = await requestAuthGet(url);

        if(response.code > 204){

            toast.error(response.errors);
            await dispatch(useActions.getUserFailture(response.errors));
        }
        else{
            localStorage.setItem('__user', JSON.stringify(response.data));
            dispatch(useActions.getUserSuccess(response.data));
        }
    }
}

function password(email) {
    return async dispatch => {

        await dispatch(useActions.passwordRequest());
        
        const url = `/client/password?email=${email}`;
        const response = await requestGet(url);

        if(response.code > 204){
            toast.error(response.errors);
            await dispatch(useActions.passwordFailture(response.errors));
        }
        else{
            await dispatch(useActions.passwordSuccess());
        }
    }
}

function updateProfile(user, user_uid) {
    return async dispatch => {
        await dispatch(useActions.profileRequest());
        
        const url = `/client/${user_uid}`;
        const response = await requestAuthPost(url, user);

        if(response.code > 204){
            
            toast.error(response.errors, {
                closeOnClick: true,
                closeButton: true,
                autoClose: false
            });
            await dispatch(useActions.profileSuccess());
        }
        else{
            const userStorage = JSON.parse(localStorage.getItem("__user"));
            await dispatch(useActions.profileSuccess());

            if(response.data?.email !== userStorage?.email){
                toast.success("Hết phiên đăng nhập! Bạn hãy đăng nhập lại!", {
                    autoClose: false
                });
                return "logout";
            }

            toast.success("Cập nhật thông tin thành công!");
            
            localStorage.setItem("__user", JSON.stringify(response.data));
            await dispatch(useActions.getUserSuccess(response.data));
        }
        return "OK";
    }
}

function changePassword(user_uid, payload) {
    return async dispatch => {

        await dispatch(useActions.changePasswordRequest());
        
        const url = `/client/${user_uid}`;

        const response = await requestAuthPost(url, payload);

        if(response.code > 204){
            toast.error(response.errors);
            await dispatch(useActions.changePasswordSuccess());
        }
        else{
            toast.success("Thay đổi mật khẩu thành công!");
            const payloadResponse = response.data;

            cookies.setCookie("ac_user", payloadResponse.token, 30);
            cookies.setCookie("rf_user", payloadResponse.refresh_token, 30);

            await dispatch(useActions.changePasswordSuccess());
        }
    }
}
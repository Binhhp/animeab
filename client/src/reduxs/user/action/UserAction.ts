import { userConstants } from "./UserType"
export const useActions = {
    loginClear: () => {
        return{
            type: userConstants.LOGIN_CLEAR
        }
    },
    loginRequest: () => {
        return{
            type: userConstants.LOGIN_REQUEST
        }
    },
    loginSuccess: (user: any) => {
        return{
            type: userConstants.LOGIN_SUCCESS,
            payload: user
        }
    },
    loginFailture: (errors: string) => {
        return{
            type: userConstants.LOGIN_FAILURE,
            payload: errors
        }
    },

    registerRequest: () => {
        return{
            type: userConstants.REGISTER_REQUEST
        }
    },
    registerSuccess: () => {
        return{
            type: userConstants.REGISTER_SUCCESS
        }
    },

    registerFailture: (errors?: string) => {
        return{
            type: userConstants.REGISTER_FAILURE,
            payload: errors
        }
    },
    getUserClear: () => {
        return{
            type: userConstants.GETALL_CLEAR
        }
    },
    getUserRequest: () => {
        return{
            type: userConstants.GETALL_REQUEST
        }
    },
    getUserSuccess: (user: any) => {
        return{
            type: userConstants.GETALL_SUCCESS,
            payload: user
        }
    },

    getUserFailture: (errors: string) => {
        return{
            type: userConstants.GETALL_FAILURE,
            payload: errors
        }
    },

    passwordRequest: () => {
        return{
            type: userConstants.PASSWORD_REQUEST
        }
    },
    passwordSuccess: () => {
        return{
            type: userConstants.PASSWORD_SUCCESS
        }
    },

    passwordFailture: (errors: string) => {
        return{
            type: userConstants.PASSWORD_FAILURE,
            payload: errors
        }
    },
    passwordClear: () => {
        return{
            type: userConstants.PASSWORD_CLEAR
        }
    },

    profileRequest: () => {
        return{
            type: userConstants.PROFILE_REQUEST
        }
    },
    profileSuccess: () => {
        return{
            type: userConstants.PROFILE_SUCCESS
        }
    },

    changePasswordRequest: () => {
        return{
            type: userConstants.CHANGE_PASSWORD_REQUEST
        }
    },
    changePasswordSuccess: () => {
        return{
            type: userConstants.CHANGE_PASSWORD_SUCCESS
        }
    }
}
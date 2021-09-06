import { userConstants } from "../action/UserType";

let storage = JSON.parse(localStorage.getItem('__user'));

const initialState = storage ? storage : {};
export function UserReducer (state = initialState, action) {
    switch (action.type) {
        case userConstants.GETALL_CLEAR:
            return {};
            
        case userConstants.GETALL_REQUEST:
            return {
                loading: true
            };
        case userConstants.GETALL_SUCCESS:
            var user = Object.assign({}, action.payload);
            return user;

        case userConstants.GETALL_FAILURE:
            return { 
                errors: action.payload
            };
        default : 
            return state
    }
}

export function UserProfileReducer (state = {}, action) {
    switch (action.type) {
        case userConstants.PROFILE_REQUEST:
            return {
                loading: true
            };
            
        case userConstants.PROFILE_SUCCESS:
            return {};
        default : 
            return state
    }
}

export function UserChangePasswordReducer (state = {}, action) {
    switch (action.type) {
        case userConstants.CHANGE_PASSWORD_REQUEST:
            return {
                loading: true
            };
            
        case userConstants.CHANGE_PASSWORD_SUCCESS:
            return {};
        default : 
            return state
    }
}
import { Action } from './../../interface/domain';
import { userConstants } from "../action/UserType";

let user = JSON.parse(localStorage.getItem('LOGIN_INFO') as any);

const initialState = user ? { loggedIn: true, user } : {};
export function AuthenticateReducer(state = initialState, action: Action) {
    switch (action.type) {
      case userConstants.LOGIN_CLEAR:
          let newArr = {};
          return newArr;
          
      case userConstants.LOGIN_REQUEST:
        return {
          loggingIn: true,
          user: action.payload
        };
      case userConstants.LOGIN_SUCCESS:
        return {
          loggedIn: true,
          user: action.payload
        };
      case userConstants.LOGIN_FAILURE:
        return {
            error: action.payload
        };
        
      default:
        return state
    }
  } 
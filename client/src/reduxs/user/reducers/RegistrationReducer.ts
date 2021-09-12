import { Action } from './../../interface/domain';
import { userConstants } from "../action/UserType";

export function RegistrationReducer(state = {}, action: Action) {
    switch (action.type) {
      case userConstants.REGISTER_REQUEST:
        return { registering: true };
      case userConstants.REGISTER_SUCCESS:
        return {};
      case userConstants.REGISTER_FAILURE:
        return {
          errors: action.payload
        };
      default:
        return state
    }
  }
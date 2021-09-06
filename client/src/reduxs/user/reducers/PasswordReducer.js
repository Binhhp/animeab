import { userConstants } from "../action/UserType";

export function PasswordReducer(state = {}, action) {
    switch (action.type) {
      case userConstants.PASSWORD_CLEAR:
          return {};
      case userConstants.PASSWORD_REQUEST:
        return { loading: true };
      case userConstants.PASSWORD_SUCCESS:
        var newArr = Object.assign({}, {
          success: true
        });

        return newArr;
      case userConstants.PASSWORD_FAILURE:
        return {
          errors: action.payload
        };
      default:
        return state
    }
  }
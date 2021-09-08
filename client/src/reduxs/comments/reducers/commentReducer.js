import { constants } from "../actions/comment.constants";

export function commentReducer (state = {data: []}, action) {
    switch (action.type) {
        case constants.REQUEST:
            return {
                loading: true
            };
            
        case constants.SUCCESS:
            var comments = Object.assign({}, {
                data: [...action.payload]
            });

            return comments;
        case constants.UPDATE:
            let checkData = state.data.filter(x => x.key === action.payload.key);
            if(checkData.length > 0) return state;
            let updateState = {
                data: [
                    action.payload,
                    ...state.data
                ]
            }
            return updateState;
        
        case constants.LIKE:
            const newArr = state?.data.filter(x => x?.key !== action.payload.key);
            if(newArr) {
                let rfState = {
                    data: [
                        action.payload,
                        ...newArr
                    ]
                }
                return rfState;
            }
            return state;
            
        default : 
            return state
    }
}

export function sendMessageReducer (state = {}, action) {
    switch (action.type) {
        case constants.SEND_REQUEST:
            return {
                loading: true
            };
            
        case constants.SEND_SUCCESS:
            return {};

        default : 
            return state
    }
}
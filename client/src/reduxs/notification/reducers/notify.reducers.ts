import { Action } from './../../interface/domain';

import { notifyConstants } from "../actions/notify.constants";

export const notifyReducers = (state = { data: [] }, action: Action) =>{

    switch (action.type) {
        case notifyConstants.NOTIFY_REQUEST:
            return {
                loading: true
            };
            
        case notifyConstants.NOTIFY_SUCCESS:
            var notifies = Object.assign({}, {
                data: [...action.payload]
            });

            return notifies;

        case notifyConstants.NOTIFY_UPDATE:
            if(state?.data.length > 0)
            {
                let checkData = state.data.filter((x: any) => x.key === action.payload.key);
                if(checkData.length > 0) return state;
            }
            let updateState = {
                data: [
                    action.payload,
                    ...state.data
                ]
            }
            return updateState;
        default: return state;
    }
}


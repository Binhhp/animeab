import { Action } from './../../interface/domain';
import { notifyConstants } from "../actions/notify.constants";

export const notifyCountReducers = (state = {count: 0}, action: Action) =>{

    switch (action.type) {
            
        case notifyConstants.NOTIFY_COUNT_SUCCESS:
            var notifies = Object.assign({}, {
                count: action.payload
            });

            return notifies;

        case notifyConstants.NOTIFY_COUNT_UPDATE:
            
            let updateState = {
                count: state.count + 1
            }
            return updateState;
            
        default: return state;
    }
}
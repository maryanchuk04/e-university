import { createReducer, on } from '@ngrx/store';

import { ScheduleGatewayView } from '../../models/schedule';
import * as ScheduleActions from './schedule.actions';

export interface State {
    schedule: ScheduleGatewayView | null;
    error: any;
}

export const initialState: State = {
    schedule: null,
    error: null,
};

export const scheduleReducer = createReducer(
    initialState,
    on(ScheduleActions.loadScheduleSuccess, (state, { schedule }) => ({
        ...state,
        schedule,
        error: null,
    })),
    on(ScheduleActions.loadScheduleFailure, (state, { error }) => ({
        ...state,
        schedule: null,
        error,
    }))
);

import { ActionReducerMap, MetaReducer } from '@ngrx/store';

import * as schedule from './schedule/schedule.reducers';
import * as student from './student/student.reducers';

export interface State {
    student: student.State;
    schedule: schedule.State;
}

export const reducers: ActionReducerMap<State> = {
    student: student.studentReducer,
    schedule: schedule.scheduleReducer,
};

export const metaReducers: MetaReducer<State>[] = [];

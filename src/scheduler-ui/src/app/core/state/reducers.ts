import { ActionReducerMap, MetaReducer } from '@ngrx/store';

import * as student from './student/student.reducers';

export interface State {
    student: student.State;
}

export const reducers: ActionReducerMap<State> = {
    student: student.studentReducer,
};

export const metaReducers: MetaReducer<State>[] = [];

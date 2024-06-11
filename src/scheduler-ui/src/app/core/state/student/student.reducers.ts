import { createReducer, on } from '@ngrx/store';

import { MyDayGatewayView } from '../../models/my-day-gateway-view';
import { StudentGatewayView } from '../../models/student-gateway-view';
import * as StudentActions from './student.actions';

export interface State {
    student: StudentGatewayView | null;
    myDay: MyDayGatewayView | null;
    error: string | null;
}

export const initialState: State = {
    student: null,
    myDay: null,
    error: null,
};

export const studentReducer = createReducer(
    initialState,
    on(StudentActions.loadStudentSuccess, (state, { student }) => ({
        ...state,
        student,
        error: null,
    })),
    on(StudentActions.loadStudentFailure, (state, { error }) => ({
        ...state,
        student: null,
        error,
    })),
    on(StudentActions.loadMyDaySuccess, (state, { myDay }) => ({
        ...state,
        myDay,
        error: null,
    })),
    on(StudentActions.loadMyDayFailure, (state, { error }) => ({
        ...state,
        myDay: null,
        error,
    }))
);

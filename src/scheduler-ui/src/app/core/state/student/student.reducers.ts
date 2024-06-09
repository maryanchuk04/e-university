import { createReducer, on } from '@ngrx/store';

import { StudentGatewayView } from '../../models/student-gateway-view';
import * as StudentActions from './student.actions';

export interface State {
    student: StudentGatewayView | null;
    error: string | null;
}

export const initialState: State = {
    student: null,
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
    }))
);

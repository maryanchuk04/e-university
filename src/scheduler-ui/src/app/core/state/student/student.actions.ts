import { createAction, props } from '@ngrx/store';

import { StudentGatewayView } from '../../models/student-gateway-view';

export const loadStudent = createAction('[Student] Load Student');
export const loadStudentSuccess = createAction(
    '[Student] Load Student Success',
    props<{ student: StudentGatewayView }>()
);
export const loadStudentFailure = createAction(
    '[Student] Load Student Failure',
    props<{ error: any }>()
);

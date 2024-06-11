import { createAction, props } from '@ngrx/store';

import { MyDayGatewayView } from '../../models/my-day-gateway-view';
import { StudentGatewayView } from '../../models/student-gateway-view';

// Load student
export const loadStudent = createAction('[Student] Load Student');
export const loadStudentSuccess = createAction(
    '[Student] Load Student Success',
    props<{ student: StudentGatewayView }>()
);
export const loadStudentFailure = createAction(
    '[Student] Load Student Failure',
    props<{ error: any }>()
);

// Load My day section
export const loadMyDay = createAction('[MyDay] Load My Day');
export const loadMyDaySuccess = createAction(
    '[MyDay] Load My Day Success',
    props<{ myDay: MyDayGatewayView }>()
);
export const loadMyDayFailure = createAction(
    '[MyDay] Load My Day Failure',
    props<{ error: any }>()
);


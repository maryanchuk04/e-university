import { createAction, props } from '@ngrx/store';

import { ScheduleGatewayView } from '../../models/schedule';

export const loadSchedule = createAction(
    '[Schedule] Load Schedule',
    props<{ semesterId: string; facultyId: string }>()
);

export const loadScheduleSuccess = createAction(
    '[Schedule] Load Schedule Success',
    props<{ schedule: ScheduleGatewayView }>()
);

export const loadScheduleFailure = createAction(
    '[Schedule] Load Schedule Failure',
    props<{ error: any }>()
);

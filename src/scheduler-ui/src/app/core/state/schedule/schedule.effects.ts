import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { ScheduleService } from '../../services/schedule.service';
import * as ScheduleActions from './schedule.actions';

@Injectable()
export class ScheduleEffects {
    loadSchedule$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ScheduleActions.loadSchedule),
            mergeMap(action =>
                this.scheduleService.getSemesterScheduleForFaculty(action.facultyId).pipe(
                    map(schedule =>
                        ScheduleActions.loadScheduleSuccess({ schedule })
                    ),
                    catchError(error =>
                        of(ScheduleActions.loadScheduleFailure({ error }))
                    )
                )
            )
        )
    );

    constructor(
        private actions$: Actions,
        private scheduleService: ScheduleService
    ) {}
}

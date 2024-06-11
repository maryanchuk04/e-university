import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { ScheduleService } from '../../services/schedule.service';
import { StudentService } from '../../services/student.service';
import * as ScheduleActions from '../schedule/schedule.actions';
import * as StudentActions from './student.actions';

@Injectable()
export class StudentEffects {
    loadStudent$ = createEffect(() =>
        this.actions$.pipe(
            ofType(StudentActions.loadStudent),
            mergeMap(() =>
                this.studentService.getCurrentStudent().pipe(
                    map(student =>
                        StudentActions.loadStudentSuccess({ student })
                    ),
                    catchError(error =>
                        of(StudentActions.loadStudentFailure({ error }))
                    )
                )
            )
        )
    );

    loadMyDay$ = createEffect(() =>
        this.actions$.pipe(
            ofType(StudentActions.loadMyDay),
            mergeMap(() =>
                this.studentService.getStudentDay().pipe(
                    map(myDay => StudentActions.loadMyDaySuccess({ myDay })),
                    catchError(error => of(StudentActions.loadMyDayFailure({ error })))
                )
            )
        )
    );

    loadStudentSuccess$ = createEffect(() =>
        this.actions$.pipe(
            ofType(StudentActions.loadStudentSuccess),
            mergeMap(({ student }) =>
                this.scheduleService.getSemesterScheduleForFaculty(student.facultyId).pipe(
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
        private studentService: StudentService,
        private scheduleService: ScheduleService
    ) {}
}

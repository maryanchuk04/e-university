import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { StudentService } from '../../services/student.service';
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

    constructor(
        private actions$: Actions,
        private studentService: StudentService
    ) {}
}

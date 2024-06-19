import { of } from 'rxjs';
import { catchError, map, mergeMap } from 'rxjs/operators';

import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';

import { ManagerService } from '../../core/services/manager.service';
import * as ManagerActions from './workspace.actions';

@Injectable()
export class WorkspaceEffects {
    loadManagers$ = createEffect(() =>
        this.actions$.pipe(
            ofType(ManagerActions.loadManager),
            mergeMap(() =>
                this.managerService.getCurrentManager().pipe(
                    map(manager =>
                        ManagerActions.loadManagerSuccess({ manager })
                    ),
                    catchError(error =>
                        of(ManagerActions.loadManagerFailure({ error }))
                    )
                )
            )
        )
    );

    constructor(
        private actions$: Actions,
        private managerService: ManagerService
    ) {}
}

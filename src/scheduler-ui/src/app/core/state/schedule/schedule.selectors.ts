import { createFeatureSelector, createSelector } from '@ngrx/store';

import { State } from './schedule.reducers';

export const selectScheduleState = createFeatureSelector<State>('schedule');

export const selectSchedule = createSelector(
    selectScheduleState,
    (state: State) => state.schedule
);

export const selectScheduleError = createSelector(
    selectScheduleState,
    (state: State) => state.error
);

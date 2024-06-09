import { createFeatureSelector, createSelector } from '@ngrx/store';

import * as fromStudent from './student.reducers';

export const selectStudentState = createFeatureSelector<fromStudent.State>('student');

export const selectStudent = createSelector(
  selectStudentState,
  (state: fromStudent.State) => state.student
);

export const selectStudentError = createSelector(
  selectStudentState,
  (state: fromStudent.State) => state.error
);

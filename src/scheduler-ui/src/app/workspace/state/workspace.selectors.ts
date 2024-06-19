import { createFeatureSelector, createSelector } from '@ngrx/store';

import { WorkspaceState } from './workspace.reducers';

export const selectManagerState = createFeatureSelector<WorkspaceState>('workspace');

export const selectManager = createSelector(
  selectManagerState,
  state => state.manager
);

export const selectManagerLoading = createSelector(
  selectManagerState,
  state => state.loading
);

export const selectManagerError = createSelector(
  selectManagerState,
  state => state.error
);

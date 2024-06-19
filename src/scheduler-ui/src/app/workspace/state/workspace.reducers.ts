import { createReducer, on } from '@ngrx/store';

import { ManagerGatewayView } from '../../core/models/manager-gateway-view';
import * as WorkspaceActions from './workspace.actions';

export interface WorkspaceState {
    manager: ManagerGatewayView;
    loading: boolean;
    error: any;
    managedFacultyId: string | null;
}

export const initialState: WorkspaceState = {
    manager: null,
    loading: false,
    error: null,
    managedFacultyId: null,
};

export const workspaceReducers = createReducer(
    initialState,

    on(WorkspaceActions.loadManager, state => ({
        ...state,
        loading: true,
        error: null,
    })),

    on(WorkspaceActions.loadManagerSuccess, (state, { manager }) => ({
        ...state,
        manager: manager,
        loading: false,
        error: null,
    })),

    on(WorkspaceActions.loadManagerFailure, (state, { error }) => ({
        ...state,
        loading: false,
        error: error,
    })),

    on(WorkspaceActions.updateManagedFacultyId, (state, { facultyId }) => ({
        ...state,
        managedFacultyId: facultyId,
    }))
);

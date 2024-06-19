import { createAction, props } from '@ngrx/store';

import { ManagerGatewayView } from '../../core/models/manager-gateway-view';

export const loadManager = createAction('[Manager] Load Manager');
export const loadManagerSuccess = createAction(
    '[Manager] Load Manager Success',
    props<{ manager: ManagerGatewayView }>()
);
export const loadManagerFailure = createAction(
    '[Manager] Load Manager Failure',
    props<{ error: any }>()
);


export const updateManagedFacultyId = createAction(
    '[Manager] Update Managed Faculty Id',
    props<{ facultyId: string }>()
  );

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ScheduleManagementComponent } from './schedule-management.component';

const scheduleManagementRoutes: Routes = [
    {
        path: '',
        component: ScheduleManagementComponent,
        children: [],
    },
];
@NgModule({
    imports: [RouterModule.forChild(scheduleManagementRoutes)],
    exports: [RouterModule],
})
export class ScheduleManagementRoutingModule {}

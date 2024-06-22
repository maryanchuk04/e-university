import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { WorkspaceComponent } from './workspace.component';

const routes: Routes = [
    {
        path: '',
        component: WorkspaceComponent,
        children: [
            {
                path: 'users-management',
                loadChildren: () =>
                    import(
                        './users-management/users-management-routing.module'
                    ).then(m => m.UsersManagementRoutingModule),
            },
            {
                path: 'schedule-management',
                loadChildren: () => import ('./schedule/schedule-routing.module').then(m => m.ScheduleRoutingModule),
            }
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class WorkspaceRoutingModule {}

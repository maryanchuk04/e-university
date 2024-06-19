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
        ],
    },
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class WorkspaceRoutingModule {}

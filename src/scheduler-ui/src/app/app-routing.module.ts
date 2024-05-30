import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthenticateComponent } from './auth/authenticate/authenticate.component';

const routes: Routes = [
    {
        path: '',
        redirectTo: '/dashboard',
        pathMatch: 'full'
    },
    {
        path: 'authenticate',
        component: AuthenticateComponent,
    },
    {
        path: 'dashboard',
        loadChildren: () => import('./dashboard/dashboard-routing.module').then((m) => m.DashboardRoutingModule),
    },
    {
        path: 'schedule',
        loadChildren: () => import('./schedule/schedule-routing.module').then((m) => m.ScheduleRoutingModule)
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }

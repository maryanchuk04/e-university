import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MyDayComponent } from './components/my-day/my-day.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const dashboardRoutes: Routes = [
    {
        path: '',
        component: DashboardComponent,
        children: [
            {
                path: '',
                component: MyDayComponent
            }
        ]
    },
];
@NgModule({
    imports: [RouterModule.forChild(dashboardRoutes)],
    exports: [RouterModule],
})
export class DashboardRoutingModule {}

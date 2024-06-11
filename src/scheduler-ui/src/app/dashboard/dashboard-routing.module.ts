import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TimetableComponent } from '../timetable/timetable.component';
import { MyDayComponent } from './components/my-day/my-day.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const dashboardRoutes: Routes = [
    {
        path: '',
        component: DashboardComponent,
        children: [
            {
                path: '',
                component: MyDayComponent,
            },
            {
                path: 'schedule',
                component: ScheduleComponent
            },
            {
                path: 'timetable',
                component: TimetableComponent
            },
            {
                path: 'profile/student',
                loadChildren: () => import('../student/student-routing.module').then((m) => m.StudentRoutingModule),
            }
        ]
    },
];
@NgModule({
    imports: [RouterModule.forChild(dashboardRoutes)],
    exports: [RouterModule],
})
export class DashboardRoutingModule {}

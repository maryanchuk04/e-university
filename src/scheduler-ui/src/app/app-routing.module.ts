import { NgModule } from '@angular/core';
import { mapToCanActivate, RouterModule, Routes } from '@angular/router';

import { AuthenticateComponent } from './auth/authenticate/authenticate.component';
import { AuthGuard } from './core/guards/auth.guard';
import { StudentGuard } from './dashboard/guards/student.guard';
import { TeacherGuard } from './teacher/teacher.guard';

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
        canActivate: mapToCanActivate([AuthGuard, StudentGuard])
    },
    {
        path: 'schedule',
        loadChildren: () => import('./schedule/schedule-routing.module').then((m) => m.ScheduleRoutingModule),
    },
    {
        path: 'timetable',
        loadChildren: () => import('./timetable/timetable-routing.module').then((m) => m.TimetableRoutingModule),
    },
    {
        path: 'schedule-management',
        loadChildren: () => import('./schedule-management/schedule-management-routing.module').then(m => m.ScheduleManagementRoutingModule),
        canActivate: mapToCanActivate([AuthGuard]),
    },
    {
        path: 'teacher',
        loadChildren: () => import('./teacher/teacher-routing.module').then(m => m.TeacherRoutingModule),
        canActivate: mapToCanActivate([AuthGuard, TeacherGuard]),
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }

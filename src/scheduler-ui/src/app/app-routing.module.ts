import { NgModule } from '@angular/core';
import { mapToCanActivate, RouterModule, Routes } from '@angular/router';

import { AuthenticateComponent } from './auth/authenticate/authenticate.component';
import { AdminGuard } from './core/guards/admin.guard';
import { AuthGuard } from './core/guards/auth.guard';
import { StudentGuard } from './dashboard/guards/student.guard';
import { LogoutComponent } from './logout/logout.component';
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
        path: 'full-schedule',
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
    },
    {
        path: 'workspace',
        loadChildren: () => import('./workspace/workspace-routing.module').then(m => m.WorkspaceRoutingModule),
        canActivate: mapToCanActivate([AdminGuard])
    },
    {
        path: 'logout',
        component: LogoutComponent
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule],
})
export class AppRoutingModule { }

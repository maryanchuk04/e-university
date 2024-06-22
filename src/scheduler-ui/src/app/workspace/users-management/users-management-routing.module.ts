import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { StudentsComponent } from './components/students/students.component';
import { TeachersComponent } from './components/teachers/teachers.component';
import { UserManagementComponent } from './components/user-management/user-management.component';

const routes: Routes = [
    {
        path: '',
        component: UserManagementComponent,
        children: [
            {
                path: 'students',
                component: StudentsComponent
            },
            {
                path: 'teachers',
                component: TeachersComponent
            }
        ]
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersManagementRoutingModule { }

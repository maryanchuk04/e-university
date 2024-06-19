import { SharedModule } from 'primeng/api';

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { StudentsComponent } from './components/students/students.component';
import { TeachersComponent } from './components/teachers/teachers.component';
import { UserManagementComponent } from './components/user-management/user-management.component';
import { UsersManagementRoutingModule } from './users-management-routing.module';

@NgModule({
    declarations: [
        TeachersComponent,
        StudentsComponent,
        UserManagementComponent,
    ],
    imports: [
        CommonModule,
        UsersManagementRoutingModule,
        SharedModule,
        RouterModule,
    ],
})
export class UsersManagementModule {}

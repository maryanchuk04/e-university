

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { ProfileComponent } from './profile/profile.component';
import { StudentRoutingModule } from './student-routing.module';

@NgModule({
    declarations: [ProfileComponent],
    imports: [CommonModule, StudentRoutingModule, SharedModule],
})
export class StudentModule {}

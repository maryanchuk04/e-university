import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { TeacherRoutingModule } from './teacher-routing.module';
import { TeacherComponent } from './teacher.component';

@NgModule({
    declarations: [TeacherComponent],
    imports: [CommonModule, TeacherRoutingModule],
})
export class TeacherModule {}

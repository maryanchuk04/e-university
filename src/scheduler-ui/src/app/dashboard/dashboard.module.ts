import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { StudentModule } from '../student/student.module';
import { TimetableModule } from '../timetable/timetable.module';
import { MyDayComponent } from './components/my-day/my-day.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LessonInfoModalComponent } from './lesson-info-modal/lesson-info-modal.component';

@NgModule({
    declarations: [
        DashboardComponent,
        MyDayComponent,
        LessonInfoModalComponent,
        ScheduleComponent,
    ],
    imports: [CommonModule, DashboardRoutingModule, SharedModule, TimetableModule, StudentModule],
})
export class DashboardModule {}

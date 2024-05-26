import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { MyDayComponent } from './components/my-day/my-day.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard/dashboard.component';
import { LessonInfoModalComponent } from './lesson-info-modal/lesson-info-modal.component';

@NgModule({
    declarations: [DashboardComponent, MyDayComponent, LessonInfoModalComponent],
    imports: [CommonModule, DashboardRoutingModule, SharedModule],
})
export class DashboardModule { }

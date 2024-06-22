import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../../shared/shared.module';
import { ScheduleRoutingModule } from './schedule-routing.module';
import { ScheduleComponent } from './schedule/schedule.component';

@NgModule({
    declarations: [ScheduleComponent],
    imports: [CommonModule, ScheduleRoutingModule, SharedModule],
})
export class ScheduleModule {}

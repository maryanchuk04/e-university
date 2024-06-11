import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { SharedModule } from '../shared/shared.module';
import { TimetableRoutingModule } from './timetable-routing.module';
import { TimetableComponent } from './timetable.component';

@NgModule({
    declarations: [TimetableComponent],
    imports: [CommonModule, TimetableRoutingModule, SharedModule],
})
export class TimetableModule {}

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { TimetableRoutingModule } from './timetable-routing.module';
import { TimetableComponent } from './timetable.component';

@NgModule({
    declarations: [TimetableComponent],
    imports: [CommonModule, TimetableRoutingModule],
})
export class TimetableModule {}

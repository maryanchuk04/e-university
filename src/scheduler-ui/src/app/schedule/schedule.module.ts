import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { SharedModule } from '../shared/shared.module';
import { MainComponent } from './components/main/main.component';
import { ScheduleComponent } from './components/schedule/schedule.component';
import { ScheduleRoutingModule } from './schedule-routing.module';

@NgModule({
    declarations: [
        ScheduleComponent,
        MainComponent
    ],
    imports: [
        CommonModule,
        ScheduleRoutingModule,
        SharedModule,
        RouterModule,
    ]
})
export class ScheduleModule { }

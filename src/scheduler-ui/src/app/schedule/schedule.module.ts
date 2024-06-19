import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';

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
        CalendarModule.forRoot({
            provide: DateAdapter,
            useFactory: adapterFactory,
        }),
    ]
})
export class ScheduleModule { }

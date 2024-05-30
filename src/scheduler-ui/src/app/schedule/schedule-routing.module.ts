import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { MainComponent } from './components/main/main.component';
import { ScheduleComponent } from './components/schedule/schedule.component';

const scheduleRoutes: Routes = [
    {
        path: '',
        component: ScheduleComponent,
        children: [
            {
                path: '',
                component: MainComponent
            }
        ]
    },
];

@NgModule({
    imports: [RouterModule.forChild(scheduleRoutes)],
    exports: [RouterModule],
})
export class ScheduleRoutingModule {}

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { TimetableComponent } from './timetable.component';

const timetableRoutes: Routes = [
    {
        path: '',
        component: TimetableComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(timetableRoutes)],
  exports: [RouterModule]
})
export class TimetableRoutingModule { }

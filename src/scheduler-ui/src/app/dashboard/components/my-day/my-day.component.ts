
import { Component, OnInit } from '@angular/core';

import { LessonType, mockMyDay } from '../../../core/models/lesson-view';
import { toENDay, toUADay } from '../../../utils/date';

@Component({
    selector: 'uni-my-day',
    templateUrl: './my-day.component.html',
    styleUrl: './my-day.component.scss'
})
export class MyDayComponent implements OnInit {
    myDay = mockMyDay;

    today: string;
    LessonType = LessonType;

    ngOnInit(): void {
        this.today = this.getToday();
    }

    getToday = () => {
        // TODO: Add dependency on choosen language
        if (true) {
            return toUADay(new Date())
        }
        return toENDay(new Date())
    };
}

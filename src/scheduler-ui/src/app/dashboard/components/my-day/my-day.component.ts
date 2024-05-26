import { DialogService } from 'primeng/dynamicdialog';

import { Component, OnInit } from '@angular/core';

import { LessonType, LessonView, mockMyDay } from '../../../core/models/lesson-view';
import { toENDay, toUADay } from '../../../utils/date';
import { LessonInfoModalComponent } from '../../lesson-info-modal/lesson-info-modal.component';

@Component({
    selector: 'uni-my-day',
    templateUrl: './my-day.component.html',
    styleUrl: './my-day.component.scss'
})
export class MyDayComponent implements OnInit {
    myDay = mockMyDay;

    today: string;
    LessonType = LessonType;

    constructor(private dialogService: DialogService) { }

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

    openLessonModal(lesson: LessonView) {
        this.dialogService.open(LessonInfoModalComponent, { data: { lesson }, header: lesson.name });
    }
}

import { parse } from 'date-fns';
import { DialogService } from 'primeng/dynamicdialog';
import { Observable } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

import { DayOfWeek } from '../../../core/models/day-of-week';
import { LessonType, MyDayGatewayView } from '../../../core/models/my-day-gateway-view';
import { LessonGatewayView } from '../../../core/models/schedule';
import { LessonStatus } from '../../../core/models/timetable-gateway-view';
import { loadMyDay } from '../../../core/state/student/student.actions';
import { selectMyDay } from '../../../core/state/student/student.selectors';
import { toENDay, toUADay } from '../../../utils/date';
import { LessonInfoModalComponent } from '../../lesson-info-modal/lesson-info-modal.component';

@Component({
    selector: 'uni-my-day',
    templateUrl: './my-day.component.html',
    styleUrls: ['./my-day.component.scss'],
})
export class MyDayComponent implements OnInit {
    myDay$: Observable<MyDayGatewayView>;

    LessonType = LessonType;
    DayOfWeek = DayOfWeek;
    LessonStatus = LessonStatus;

    today: string;

    constructor(
        private dialogService: DialogService,
        private store: Store,
        private translate: TranslateService
    ) {}

    ngOnInit(): void {
        this.store.dispatch(loadMyDay());

        this.myDay$ = this.store.select(selectMyDay);

        this.today = this.getToday();
    }

    getToday = () => {
        // TODO: Add dependency on choosen language
        if (true) {
            return toUADay(new Date());
        }
        return toENDay(new Date());
    };

    openLessonModal(lesson: LessonGatewayView) {
        this.dialogService.open(LessonInfoModalComponent, {
            data: { lesson },
            header: lesson.lessonName,
        });
    }

    getNextLesson(lessons: LessonGatewayView[]): LessonGatewayView | null {
        const now = new Date();
        return (
            lessons.find(
                lesson =>
                    parse(lesson.startAt, 'HH:mm:ss.SSSSSSS', new Date()) > now
            ) || null
        );
    }

    getTimeUntilNextLesson(nextLesson: LessonGatewayView): string {
        const now = new Date();
        const startTime = parse(
            nextLesson.startAt,
            'HH:mm:ss.SSSSSSS',
            new Date()
        );
        const diffMs = startTime.getTime() - now.getTime();
        const diffMinutes = Math.floor(diffMs / 60000);
        const hours = Math.floor(diffMinutes / 60);
        const minutes = diffMinutes % 60;
        return `${hours}h ${minutes}m`;
    }

    isLessonPassed(lesson: LessonGatewayView): boolean {
        const now = new Date();
        const endTime = parse(lesson.endAt, 'HH:mm:ss.SSSSSSS', new Date());
        return endTime < now;
    }

    isLessonNow(lesson: LessonGatewayView): boolean {
        const now = new Date();
        const startAt = parse(lesson.startAt, 'HH:mm:ss.SSSSSSS', new Date());
        const endAt = parse(lesson.endAt, 'HH:mm:ss.SSSSSSS', new Date());
        return startAt <= now && now <= endAt;
    }

    determineLessonStatus(lesson: LessonGatewayView): LessonStatus {
        if (this.isLessonNow(lesson)) {
            return LessonStatus.Now;
        }

        if (this.isLessonPassed(lesson)) {
            return LessonStatus.Passed;
        }

        return LessonStatus.NotPassed;
    }
}

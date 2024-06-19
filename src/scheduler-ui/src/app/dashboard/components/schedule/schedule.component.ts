import { takeUntil } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

import { DayOfWeek, WeekType, workingDays } from '../../../core/models/day-of-week';
import { LessonGatewayView, ScheduleGatewayView } from '../../../core/models/schedule';
import { StudentGatewayView } from '../../../core/models/student-gateway-view';
import { selectSchedule } from '../../../core/state/schedule/schedule.selectors';
import { selectStudent } from '../../../core/state/student/student.selectors';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { toENDay, toUADay } from '../../../utils/date';

@Component({
    selector: 'uni-schedule',
    templateUrl: './schedule.component.html',
    styleUrls: ['./schedule.component.scss'],
})
export class ScheduleComponent extends BaseComponent implements OnInit {
    isLoading: boolean = true;
    stateOptions: any[];

    workingDays = workingDays;
    activeDay = DayOfWeek.Monday;

    schedule: ScheduleGatewayView;
    student: StudentGatewayView;
    lessons: LessonGatewayView[] = [];

    weekType = WeekType.Even;

    constructor(private store: Store, private translate: TranslateService) {
        super();
    }

    ngOnInit(): void {
        this.isLoading = true;
        this.translate.get(['week_type.odd', 'week_type.even']).subscribe(translations => {
            this.stateOptions = [
                { label: translations['week_type.even'], value: WeekType.Even },
                { label: translations['week_type.odd'], value: WeekType.Odd },
            ];
        });

        this.store.select(selectSchedule)
            .pipe(takeUntil(this.destroy$))
            .subscribe(scheduleRes => {
                this.schedule = scheduleRes;
                this.updateLessons();
                this.isLoading = false;
            });

        this.store.select(selectStudent)
            .pipe(takeUntil(this.destroy$))
            .subscribe(studentRes => {
                this.student = studentRes;
                this.updateLessons();
                this.isLoading = false;
            });
    }

    changeDay(day: DayOfWeek) {
        this.activeDay = day;
        this.updateLessons();
    }

    getToday = () => {
        // TODO: Add dependency on chosen language
        if (true) {
            return `Сьогодні: ${toUADay(new Date())}`;
        }
        return toENDay(new Date());
    };

    onWeekTypeChange(newWeekType: WeekType) {
        this.weekType = newWeekType;
        this.updateLessons();
    }

    updateLessons() {
        if (!this.schedule || !this.student) return;

        const groupSchedule =
            this.weekType === WeekType.Even
                ? this.schedule.evenWeek.groupsSchedule
                : this.schedule.oddWeek.groupsSchedule;
        if (!groupSchedule) return;

        const group = groupSchedule.find(
            x => x.groupId === this.student.groupId
        );

        if (!group) {
            this.lessons = [];
            return;
        }

        const daySchedule = group.days.find(d => d.day === this.activeDay);
        this.lessons = daySchedule ? daySchedule.lessons : [];
    }
}

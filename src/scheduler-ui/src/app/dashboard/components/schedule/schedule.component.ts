import Hammer from 'hammerjs';
import { takeUntil } from 'rxjs';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';

import { DayOfWeek, WeekType, workingDays } from '../../../core/models/day-of-week';
import { LessonType } from '../../../core/models/my-day-gateway-view';
import { LessonGatewayView, ScheduleGatewayView } from '../../../core/models/schedule';
import { StudentGatewayView } from '../../../core/models/student-gateway-view';
import { MediaService, ScreenSize } from '../../../core/services/media.service';
import { selectSchedule } from '../../../core/state/schedule/schedule.selectors';
import { selectStudent } from '../../../core/state/student/student.selectors';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { toENDay, toUADay } from '../../../utils/date';

export enum ViewType {
    Week = 'week',
    Day = 'day',
}

@Component({
    selector: 'uni-schedule',
    templateUrl: './schedule.component.html',
    styleUrls: ['./schedule.component.scss'],
})
export class ScheduleComponent extends BaseComponent implements OnInit {
    isLoading: boolean = true;
    isSidebarVisible = false;
    isViewTypeDisabled = false;

    sidebarLesson: LessonGatewayView;
    schedule: ScheduleGatewayView;
    student: StudentGatewayView;
    lessons: LessonGatewayView[] = [];

    weekTypeOptions: any[];
    viewTypeOptions: any[];

    ViewType = ViewType;
    LessonType = LessonType;

    workingDays = workingDays;
    activeDay = DayOfWeek.Monday;
    weekType = WeekType.Even;
    viewType = ViewType.Week;

    constructor(private store: Store, private translate: TranslateService, private media: MediaService) {
        super();
    }

    ngOnInit(): void {
        this.media.getScreenSize().subscribe(res => {
            if (res === ScreenSize.XS || res === ScreenSize.MD) {
                this.viewType = ViewType.Day;
                this.isViewTypeDisabled = true;
            }
            else this.isViewTypeDisabled = false;
        })

        this.isLoading = true;
        this.weekTypeOptions = [
            {
                label: this.translate.instant('week_type.even'),
                value: WeekType.Even,
            },
            {
                label: this.translate.instant('week_type.odd'),
                value: WeekType.Odd,
            },
        ];

        this.viewTypeOptions = [
            {
                label: this.translate.instant('view_type.day'),
                value: ViewType.Day,
            },
            {
                label: this.translate.instant('view_type.week'),
                value: ViewType.Week,
            },
        ];

        this.store
            .select(selectSchedule)
            .pipe(takeUntil(this.destroy$))
            .subscribe(scheduleRes => {
                if (scheduleRes) {
                    this.schedule = scheduleRes;
                    this.updateLessons();
                    this.isLoading = false;
                }
            });

        this.store
            .select(selectStudent)
            .pipe(takeUntil(this.destroy$))
            .subscribe(studentRes => {
                this.student = studentRes;
                this.updateLessons();
            });

        this.initializeSwipeListeners();
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

    onViewTypeChange(view: ViewType) {
        this.viewType = view;
        // this.updateLessons();
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

    showLesson(lesson: LessonGatewayView) {
        this.sidebarLesson = lesson;
        this.isSidebarVisible = true;
    }

    private initializeSwipeListeners() {
        const element = document.querySelector('uni-schedule') as HTMLElement;
        if (element) {
            const hammer = new Hammer(element);
            hammer.on('swipeleft', () => this.changeToNextDay());
            hammer.on('swiperight', () => this.changeToPreviousDay());
        }
    }

    private changeToNextDay() {
        const currentIndex = this.workingDays.indexOf(this.activeDay);
        const nextIndex = (currentIndex + 1) % this.workingDays.length;
        this.changeDay(this.workingDays[nextIndex]);
    }

    private changeToPreviousDay() {
        const currentIndex = this.workingDays.indexOf(this.activeDay);
        const previousIndex =
            (currentIndex - 1 + this.workingDays.length) %
            this.workingDays.length;
        this.changeDay(this.workingDays[previousIndex]);
    }
}

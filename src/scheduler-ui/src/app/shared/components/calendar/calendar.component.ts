import { CalendarEvent } from 'angular-calendar';
import { addDays, addMinutes, parse, startOfWeek } from 'date-fns';

import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

import { workingDays } from '../../../core/models/day-of-week';
import {
    DayScheduleGatewayView, GroupScheduleGatewayView, LessonGatewayView, ScheduleGatewayView
} from '../../../core/models/schedule';

@Component({
    selector: 'uni-calendar',
    templateUrl: './calendar.component.html',
    styleUrls: ['./calendar.component.scss'],
})
export class CalendarComponent implements OnInit {
    @Input() schedule: ScheduleGatewayView;

    @Output() onEventClick = new EventEmitter<LessonGatewayView>();

    workingDays = workingDays;

    viewDate: Date = new Date();
    events: CalendarEvent[] = [];
    daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    excludeDays: number[] = [0, 6];

    constructor() {}

    ngOnInit(): void {
        this.events = this.transformScheduleToEvents(this.schedule);
    }

    transformScheduleToEvents(schedule: ScheduleGatewayView): CalendarEvent[] {
        const events: CalendarEvent<LessonGatewayView>[] = [];

        const processWeek = (week: any) => {
            week.groupsSchedule.forEach(
                (groupSchedule: GroupScheduleGatewayView) => {
                    groupSchedule.days.forEach(
                        (daySchedule: DayScheduleGatewayView) => {
                            const dayIndex = daySchedule.day;
                            daySchedule.lessons.forEach(
                                (lesson: LessonGatewayView) => {
                                    const startTime = parse(
                                        lesson.startAt,
                                        'HH:mm:ss.SSSSSSS',
                                        new Date()
                                    );
                                    const endTime = parse(
                                        lesson.endAt,
                                        'HH:mm:ss.SSSSSSS',
                                        new Date()
                                    );
                                    const startOfCurrentWeek = startOfWeek(
                                        this.viewDate
                                    );
                                    const lessonStart = addMinutes(
                                        addDays(startOfCurrentWeek, dayIndex),
                                        startTime.getHours() * 60 +
                                            startTime.getMinutes()
                                    );
                                    const lessonEnd = addMinutes(
                                        addDays(startOfCurrentWeek, dayIndex),
                                        endTime.getHours() * 60 +
                                            endTime.getMinutes()
                                    );
                                    events.push({
                                        start: lessonStart,
                                        end: lessonEnd,
                                        title: lesson.lessonName,
                                        meta: { ...lesson },
                                    });
                                }
                            );
                        }
                    );
                }
            );
        };
        processWeek(schedule.evenWeek);
        processWeek(schedule.oddWeek);

        return events;
    }

    toggleEvent({ event }) {
        this.onEventClick.emit(event.meta);
    }
}

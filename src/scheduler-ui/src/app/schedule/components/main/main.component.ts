import { CalendarEvent } from 'angular-calendar';
import { addMinutes, parse, startOfDay } from 'date-fns';

import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';

import { workingDays } from '../../../core/models/day-of-week';
import { LessonGatewayView, ScheduleGatewayView } from '../../../core/models/schedule';
import { loadSchedule } from '../../../core/state/schedule/schedule.actions';
import { selectSchedule } from '../../../core/state/schedule/schedule.selectors';

@Component({
    selector: 'uni-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
    workingDays = workingDays

    viewDate: Date = new Date();
    events: CalendarEvent[] = [];
    daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday'];
    excludeDays: number[] = [0, 6];

    constructor(private store: Store) {}

    ngOnInit(): void {
        this.store.dispatch(
            loadSchedule({
                facultyId: '4ae60761-5a0d-40e8-9dd7-98832d93771c',
                semesterId: '',
            })
        );

        this.store.select(selectSchedule).subscribe(schedule => {
            this.events = this.transformScheduleToEvents(schedule);
        });
    }

    transformScheduleToEvents(schedule: ScheduleGatewayView): CalendarEvent[] {
        const events: CalendarEvent<LessonGatewayView>[] = [];

        const processWeek = (week: any) => {
            week.groupsSchedule.forEach((groupSchedule: any) => {
                groupSchedule.days.forEach((daySchedule: any) => {
                    daySchedule.lessons.forEach((lesson: LessonGatewayView) => {
                        const dayIndex = daySchedule.day; // Використовуємо daySchedule.day напряму
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

                        events.push({
                            start: addMinutes(
                                startOfDay(new Date()),
                                dayIndex * 1440 +
                                    startTime.getHours() * 60 +
                                    startTime.getMinutes()
                            ),
                            end: addMinutes(
                                startOfDay(new Date()),
                                dayIndex * 1440 +
                                    endTime.getHours() * 60 +
                                    endTime.getMinutes()
                            ),
                            title: lesson.lessonName,
                            meta: {
                                teacherName: lesson.teacherName,
                                roomName: lesson.roomName,
                                isOnline: lesson.isOnline,
                                url: lesson.url,
                            } as LessonGatewayView,
                        });
                    });
                });
            });
        };

        processWeek(schedule.evenWeek);
        processWeek(schedule.oddWeek);

        return events;
    }
}

import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { TimeTableGatewayView } from '../../core/models/timetable-gateway-view';
import { BaseHttpService } from '../../core/services/base.service';

@Injectable({
    providedIn: 'root',
})
export class TimetableService extends BaseHttpService {
    url = `api/faculty`;

    constructor(http: HttpClient, cookieService: CookieService) {
        super(http, cookieService);
    }

    getFacultyTimetable(facultyId: string): Observable<TimeTableGatewayView> {
        return this.get(`${this.url}/${facultyId}/timetable`);
    }
}

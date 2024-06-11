import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { ScheduleGatewayView } from '../models/schedule';
import { BaseHttpService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class ScheduleService extends BaseHttpService {
    route = 'api/schedule';

    constructor(http: HttpClient, cookieService: CookieService) {
        super(http, cookieService);
    }

    getSemesterScheduleForFaculty(
        facultyId: string
    ): Observable<ScheduleGatewayView> {
        return this.get(`${this.route}/faculty/${facultyId}`);
    }
}

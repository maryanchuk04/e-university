import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { MyDayGatewayView } from '../models/my-day-gateway-view';
import { StudentGatewayView } from '../models/student-gateway-view';
import { BaseHttpService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class StudentService extends BaseHttpService {
    url = `api/student`;

    constructor(http: HttpClient, cookieService: CookieService) {
        super(http, cookieService);
    }

    getCurrentStudent(): Observable<StudentGatewayView> {
        return this.get<StudentGatewayView>(this.url);
    }

    getStudentDay(): Observable<MyDayGatewayView> {
        return this.get<MyDayGatewayView>(`${this.url}/my-day`);
    }
}

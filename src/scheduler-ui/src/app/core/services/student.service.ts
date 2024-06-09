import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { StudentGatewayView } from '../models/student-gateway-view';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root',
})
export class StudentService extends BaseService {
    url = `api/student`;

    constructor(http: HttpClient, cookieService: CookieService) {
        super(http, cookieService);
    }

    getCurrentStudent(): Observable<StudentGatewayView> {
        return this.get<StudentGatewayView>(this.url);
    }
}
